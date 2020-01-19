using System;
using System.Reflection;
using System.Linq;
using InCore;

namespace ConInspector
{
    class Program
    {
        static Device device = null;
        static InCore.InCore core = null;
        static void Main(string[] args)
        {
            core = new InCore.InCore();
            device = core.device;
            device.ChangePropertyEvent += Device_ChangePropertyEvent;
            ShowPorts();
            core.OpenPort();
            core.Init();
            Console.WriteLine();
            Console.ReadKey();
            core.ClosePort();
        }

        private static void Device_ChangePropertyEvent(Device.DevProperties property, double value)
        {
            Console.WriteLine($"{property.ToString()} : {value.ToString()}");
        }

        private static void ShowPorts()
        {
            string[] ports = core.PortsEnabled;
            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine($"{i} : {ports[i]}");
            }
            var ch = Console.ReadKey().KeyChar;
            int num = int.Parse(ch.ToString());
            core.PortConfig.ComName = ports[num];
        }
    }
}
