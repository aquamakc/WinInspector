using System;
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
            device.ParamChanges += Device_ParamChanges;
            ShowPorts();
            core.OpenPort();
            core.Init();
            Console.ReadKey();
            core.ClosePort();
        }

        private static void Device_ParamChanges()
        {
            Console.WriteLine($"Напряжение: {device.Voltage.ToString()}");
            Console.WriteLine($"Ток: {device.Current.ToString()}");
            Console.WriteLine($"Потребляемая мощность: {device.Power.ToString()}");
            Console.WriteLine($"Частота тока: {device.Frequency.ToString()}");
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
