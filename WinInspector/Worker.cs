using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WinInspector.Forms;
using WinInspector.Tasks;
using InCore;
using static InCore.InProtocol;

namespace WinInspector
{
    public class Worker
    {
        Form mainForm = null, comConfigForm = null;
        InCore.InCore core = null;

        public Worker()
        {           
            core = new InCore.InCore();
            mainForm = new MainForm(this, core.device);           
            mainForm.Show();
        }

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

        private void Device_ParamChanges()
        {
            (mainForm as MainForm).UpdateData();
        }

        public void ShowConfigForm()
        {
            PortConfig portConfig = (core.PortConfig.Clone() as PortConfig);
            comConfigForm = new ComCofigForm(portConfig);
            if (comConfigForm.ShowDialog(mainForm) == DialogResult.OK)
                core.PortConfig = portConfig;
        }


    }
}
