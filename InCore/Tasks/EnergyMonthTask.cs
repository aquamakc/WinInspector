using InCore;
using System.IO.Ports;
using static InCore.InProtocol;

namespace WinInspector.Tasks
{
    public class EnergyMonthTask : TaskBase
    {
        public EnergyMonthTask(Device device, SerialPort port) : base(device, port) { }

        public override InProtocol.Answer DoingMethod()
        {
            byte[] OutCom = GetHandler().GetReadEnergyCommand();
            SendData(OutCom);
            return GetHandler().CheckEnergyAnswer(InData.ToArray(), Device);
        }
    }
}
