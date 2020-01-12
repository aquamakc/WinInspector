using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using static InCore.InProtocol;
using InCore;

namespace WinInspector.Tasks
{
    public class ReadVoltageTask : TaskBase
    {
        public ReadVoltageTask(Device device, SerialPort port) : base(device, port) { }

        public override Answer DoingMethod()
        {
            byte[] OutCom = GetHandler().GetVoltageCommand();
            SendData(OutCom);
            return GetHandler().CheckVoltageAnswer(InData.ToArray(), Device);
        }
    }
}
