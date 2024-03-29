﻿using InLogger;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WinInspector.Tasks;
using static InCore.InProtocol;

namespace InCore
{
    public class InCore
    {
        public Device device;
        readonly SerialPort sp = null;

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
        }

        #region ComPort

        public string[] PortsEnabled { get { return SerialPort.GetPortNames(); } }
        public string PortName { get { return sp.PortName; } set { sp.PortName = value; } }
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
                string SavingCfg = File.ReadAllText(ConfigFile);
                PortConfig = JsonConvert.DeserializeObject<PortConfig>(SavingCfg);
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
                string SavingCfg = JsonConvert.SerializeObject(PortConfig);
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
