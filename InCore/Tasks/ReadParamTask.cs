using System.IO.Ports;
using static InCore.InProtocol;
using InCore;

namespace WinInspector.Tasks
{
    public class ReadParamTask : TaskBase
    {
        public ReadParamTask(Device device, SerialPort port, Params needParam) : base(device, port, needParam) { }

        public override Answer DoingMethod()
        {
            byte[] OutCom = GetHandler().GetReadParamCommand(Parameter);
            SendData(OutCom);
            return GetHandler().CheckParamAnswer(InData.ToArray(), Device, Parameter);
        }
    }
}
