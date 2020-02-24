using System;
using System.Reflection;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using InCore;
using System.Net;

namespace ConInspector
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

        static void Main(string[] args)
        {
            core = new InCore.InCore();
            device = core.device;
            RunBot();
            device.ChangePropertyEvent += Device_ChangePropertyEvent;
            ShowPorts();
            core.OpenPort();
            core.Init();
            Console.ReadKey();
            client.StopReceiving();
            core.ClosePort();           
        }

        private static void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            Console.WriteLine($"{property.ToString()} : {value.ToString()}");
            if (Id == null)
                return;
            var res = client.SendTextMessageAsync(Id, $"{property.ToString()} : {value.ToString()}").GetAwaiter().GetResult();
        }

        private static void ShowPorts()
        {
            string[] ports = core.PortsEnabled;
            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine($"{i} : {ports[i]}");
            }         
            var ch = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            int num = int.Parse(ch.ToString());
            var config = core.PortConfig;
            config.ComName = ports[num];
            core.PortConfig = config;
            core.SaveConfig();
        }

        #region Telegram.Bot
        private static void RunBot()
        {
            wp = new WebProxy($"{proxyIp}:{proxyPort.ToString()}", true);
            //wp.Credentials = new NetworkCredential("ecvumfkl-dest", "bl87hy9rzwia");
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
            if(message.Text == "R")
            {
                client.SendTextMessageAsync(Id, message.Text);
            }
            if (message.Text == "C")
            {
                core.Init();
            }
        }

        #endregion
    }
}
