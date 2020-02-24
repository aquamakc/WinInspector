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

namespace ConDaemon
{
    class Program
    {
        static Device device = null;
        static InCore.InCore core = null;
        private static TelegramBotClient client;
        private static ChatId Id = null;
        private static WebProxy wp = null;
        private const string proxyIp = "136.243.47.220";
        private const int proxyPort = 3128;

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
            device.ChangePropertyEvent += Device_ChangePropertyEvent;
            core.OpenPort();
            await host.RunConsoleAsync();
        }

        #region Telegram.Bot
        private static void RunBot()
        {
            wp = new WebProxy($"{proxyIp}:{proxyPort.ToString()}", true);
            client = new TelegramBotClient("1005264688:AAEodWIy4O1hWhTJ66u4jtRtmcveQFfodvo", wp);
            client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.StartReceiving();
        }

        private static void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            Id = message.Chat.Id;
            if (message.Type != MessageType.Text)
                return;
            if (message.Text == "R")
            {
                client.SendTextMessageAsync(Id, message.Text);
            }
            if (message.Text == "C")
            {
                core.Init();
            }
        }

        private static void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            if (Id == null)
                return;
            var res = client.SendTextMessageAsync(Id, $"{property.ToString()} : {value.ToString()}").GetAwaiter().GetResult();
        }

        #endregion
    }
}
