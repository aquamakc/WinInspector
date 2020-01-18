using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Ports;
using WinInspector.Tasks;
using static InCore.InProtocol;

namespace InCore
{
    public class InCore
    {
        public Device device;
        SerialPort sp = null;

        private readonly string ConfigFile = Path.Combine(Environment.CurrentDirectory, "config.json");

        public InCore()
        {
            sp = new SerialPort();
            device = new Device
            {
                Adress = "101748578",
                Password = "777777"
            };
            LoadConfig();
            device.ParamChanges += Device_ParamChanges;
        }

        private void Device_ParamChanges()
        {
            //throw new NotImplementedException();
        }

        #region ComPort

        public string[] PortsEnabled { get { return SerialPort.GetPortNames(); } }
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

        public PortConfig PortConfig
        {
            get
            {
                return new PortConfig()
                {
                    ComName = PortName,
                    BaudRate = sp.BaudRate,
                    DataBits = sp.DataBits,
                    Parity = (int)sp.Parity,
                    StopBits = (int)sp.StopBits
                };
            }

            set
            {
                if (sp == null)
                    return;
                sp.PortName = value.ComName;
                sp.BaudRate = value.BaudRate;
                sp.DataBits = value.DataBits;
                sp.Parity = (Parity)value.Parity;
                sp.StopBits = (StopBits)value.StopBits;
            }
        }

        void LoadConfig()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                    return;
                String SavingCfg = File.ReadAllText(ConfigFile);
                PortConfig config = JsonConvert.DeserializeObject<PortConfig>(SavingCfg);
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

        public void SaveConfig()
        {
            try
            {
                String SavingCfg = JsonConvert.SerializeObject(PortConfig);
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
            var t = new InitTask(device, sp);
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
                new ReadParamTask(device, sp, Params.VOLTA),
                new ReadParamTask(device, sp, Params.CURRE),
                new ReadParamTask(device, sp, Params.POWEP),
                new ReadParamTask(device, sp, Params.FREQU),
                new EnergyMonthTask(device, sp)
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
