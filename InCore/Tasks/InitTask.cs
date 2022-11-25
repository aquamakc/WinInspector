using InCore;
using System.IO.Ports;
using static InCore.InProtocol;

namespace WinInspector.Tasks
{
    public class InitTask : TaskBase
    {
        public InitTask(Device device, SerialPort port) : base(device, port){}

        public override Answer DoingMethod()
        {
            byte[] OutCom = GetHandler().ConnectCommand(Device);
            SendData(OutCom);
            Answer answer = GetHandler().CheckConnectAnswer(InData.ToArray(), Device);
            if (answer != Answer.NOERR)
                return answer;
            OutCom = GetHandler().SpeedAgreementCommand(Device);
            SendData(OutCom);
            answer = GetHandler().CheckSpeedAgreement(InData.ToArray());
            if (answer != Answer.NOERR)
                return answer;
            OutCom = GetHandler().PassAgreementCommand(Device);
            SendData(OutCom);
            return GetHandler().CheckPassAgreement(InData.ToArray());
        }
    }
}
