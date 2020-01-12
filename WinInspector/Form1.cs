using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinInspector.Tasks;
using InCore;

namespace WinInspector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InCore.InCore core = new InCore.InCore();
            Device device = core.device;
            SerialPort sp = new SerialPort("COM3");
            sp.BaudRate = 9600;
            sp.DataBits = 7;
            sp.StopBits = StopBits.One;
            sp.Parity = Parity.Even;
            sp.Open();
            TaskBase b = new InitTask(device, sp);
            var res = b.Execute();
            b.Dispose();
            if (res == InProtocol.Answer.NOERR)
            {
                b = new ReadVoltageTask(device, sp);
                res = b.Execute();
                b.Dispose();
            }
            sp.Close();
        }
    }
}
