using System;
using System.Reflection;
using System.Linq;
using InCore;
using System.Net;
using System.Timers;
using Microsoft.EntityFrameworkCore;
using ConInspector.DB;
using System.IO.Ports;

namespace ConInspector
{
    class Program
    {
        static Device device = null;
        static InCore.InCore core = null;

        static Timer timer = null;

        static Context context = null;

        static void Main(string[] args)
        {
            core = new InCore.InCore();
            device = core.device;
            device.IsEconomyTraffic = false;
            device.ChangePropertyEvent += Device_ChangePropertyEvent;
            context = new Context();
            ShowPorts();
            core.OpenPort();
            timer = new Timer(10000)
            {
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Console.ReadKey();
            core.ClosePort();           
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            core.Init();
        }

        private static void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            Console.WriteLine($"{property}: {value}");
            switch (property)
            {
                case Device.DevProperties.Voltage:
                    context.Voltages.Add(new DB.DbModels.Voltage() { Value= value, CreatedAt = DateTime.Now });
                    break;
                case Device.DevProperties.Current:
                    context.Currents.Add(new DB.DbModels.Current() { Value = value, CreatedAt = DateTime.Now });
                    break;
                case Device.DevProperties.Frequency:
                    context.Frequencies.Add(new DB.DbModels.Frequency() { Value = value, CreatedAt = DateTime.Now });
                    break;
                case Device.DevProperties.Power:
                    context.Powers.Add(new DB.DbModels.Power() { Value = value, CreatedAt = DateTime.Now });
                    break;
                default:
                    return;
            }
            context.SaveChanges();
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
            config.DataBits = 7;
            config.Parity = (int)Parity.Even;
            config.ComName = ports[num];
            core.PortConfig = config;
            core.SaveConfig();
        }

    }
}
