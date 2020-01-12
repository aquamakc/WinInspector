using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
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
        SerialPort sp = null;

        private readonly string ConfigFile = Path.Combine(Environment.CurrentDirectory, "config.json");

        public Worker()
        {           
            sp = new SerialPort();
            core = new InCore.InCore();
            LoadConfig();
            core.device.ParamChanges += Device_ParamChanges;
            mainForm = new MainForm(this, core.device);           
            mainForm.Show();
        }

        private void Device_ParamChanges()
        {
            (mainForm as MainForm).UpdateData();
        }

        public void ShowConfigForm()
        {
            comConfigForm = new ComCofigForm(sp);
            if (comConfigForm.ShowDialog(mainForm) == DialogResult.OK)
                SaveConfig();
        }

        #region ComPort

        public string PortName { get { return sp.PortName; } }
        public bool IsPortOpen { get { return sp.IsOpen; } }
        public bool OpenPort()
        {
            sp.Open();
            return IsPortOpen;
        }

        public bool ClosePort()
        {
            sp.Close();
            return !IsPortOpen;
        }

        #endregion

        #region Config

        void LoadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                    return;
                String SavingCfg = File.ReadAllText(ConfigFile);
                Config config = JsonConvert.DeserializeObject<Config>(SavingCfg);
                sp.PortName = config.ComName;
                sp.BaudRate = config.BaudRate;
                sp.DataBits = config.DataBits;
                sp.Parity = (Parity)config.Parity;
                sp.StopBits = (StopBits)config.StopBits;
            }
            catch (Exception)
            {
                return;
            }
        }

        void SaveConfig()
        {
            try
            {
                Config config = new Config()
                {
                    ComName = PortName,
                    BaudRate = sp.BaudRate,
                    DataBits = sp.DataBits,
                    Parity = (int)sp.Parity,
                    StopBits = (int)sp.StopBits
                };
                String SavingCfg = JsonConvert.SerializeObject(config);
                File.WriteAllText(ConfigFile, SavingCfg);
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region WorkWithDevice

        public void Init()
        {
            var t = new InitTask(core.device, sp);
            var answer = t.Execute();
            t.Dispose();
            if (answer != Answer.NOERR)
                return;
            ReadAllParams();
        }

        public void ReadAllParams()
        {
            TaskBase[] tasks = new TaskBase[]
            {
                new ReadParamTask(core.device, sp, Params.VOLTA),
                new ReadParamTask(core.device, sp, Params.CURRE),
                new ReadParamTask(core.device, sp, Params.POWEP),
                new ReadParamTask(core.device, sp, Params.FREQU),
                new EnergyMonthTask(core.device, sp)
        };
        foreach (var task in tasks)
            {
                var answer = task.Execute();
                task.Dispose();
                if (answer != Answer.NOERR)
                    continue;
            }
        }


        #endregion

    }
}
