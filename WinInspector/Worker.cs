using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using WinInspector.Forms;
using InCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;

namespace WinInspector
{
    public class Worker
    {
        private readonly Form mainForm = null;
        private Form comConfigForm = null;
        readonly InCore.InCore core = null;

        private static TelegramBotClient client;
        private static ChatId chatId = null;

        public Worker()
        {
            core = new InCore.InCore();
            core.device.IsEconomyTraffic = false;
            core.device.ChangePropertyEvent += Device_ChangePropertyEvent;
            mainForm = new MainForm(this, core.device);
            mainForm.Show();
            RunBot();
        }

        private void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            string answer = $"{property} {value}";
            client?.SendTextMessageAsync(
                        chatId: chatId,
                        text: answer);
        }

        #region Telegramm

        readonly CancellationTokenSource cts = new CancellationTokenSource();

        private void RunBot()
        {
            client = new TelegramBotClient("1005264688:AAEodWIy4O1hWhTJ66u4jtRtmcveQFfodvo");
            client.StartReceiving(updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            cancellationToken: cts.Token);
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception.ToString();
            return Task.CompletedTask;
        }

        #endregion

        #region ComPort

        public string PortName { get { return core.PortName; } }
        public bool IsPortOpen { get { return core.IsPortOpen; } }
        public bool OpenPort()
        {
            core.OpenPort();
            return IsPortOpen;
        }

        public bool ClosePort()
        {
            core.ClosePort();
            return !IsPortOpen;
        }

        #endregion

        public void BeginReadParams()
        {
            core.Init();
        }

        public void ShowConfigForm()
        {
            PortConfig portConfig = (core.PortConfig.Clone() as PortConfig);
            comConfigForm = new ComCofigForm(portConfig);
            if (comConfigForm.ShowDialog(mainForm) == DialogResult.OK)
            {
                core.PortConfig = portConfig;
                core.SaveConfig();
            }
        }


    }
}
