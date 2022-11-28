using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using InCore;
using System.Net;
using System.Threading;

namespace ConDaemon
{
    class Program
    {
        static Device device = null;
        static InCore.InCore core = null;

        private static TelegramBotClient client;
        readonly static CancellationTokenSource cts = new CancellationTokenSource();
        private static ChatId chatId = null;

        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<DaemonConfig>(hostContext.Configuration.GetSection("Daemon"));

                    services.AddSingleton<IHostedService, DaemonService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });
            RunBot();
            core = new InCore.InCore();
            device = core.device;
            core.PortConfig.ComName = "ttyUSB0";
            device.IsEconomyTraffic = false;
            device.ChangePropertyEvent += Device_ChangePropertyEvent;
            core.OpenPort();
            await host.RunConsoleAsync();
        }

        #region Telegram.Bot
        private static void RunBot()
        {
            client = new TelegramBotClient("1005264688:AAEodWIy4O1hWhTJ66u4jtRtmcveQFfodvo");
            client.StartReceiving(updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            cancellationToken: cts.Token);
        }

        static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            chatId = message.Chat.Id;
            switch (message.Text.Trim().ToUpper())
            {
                case "C":
                    core.Init();
                    break;
                case "R":
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Report",
                        cancellationToken: cancellationToken);
                    break;
                default:
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "No command",
                        cancellationToken: cancellationToken);
                    break;
            }
        }

        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            cts.Cancel();
            return Task.CompletedTask;
        }

        private static void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            if (chatId == null)
                return;
            string answer = $"{property} {value}";
            client?.SendTextMessageAsync(
                        chatId: chatId,
                        text: answer,
                        cancellationToken: cts.Token);
        }

        #endregion
    }
}
