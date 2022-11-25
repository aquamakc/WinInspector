using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO.Ports;
using InCore;
using static InCore.InProtocol;
using System.Threading;
using System.Diagnostics;
using InLogger;

namespace WinInspector.Tasks
{
    public abstract class TaskBase : IDisposable
    {
        public TaskBase(Device device, SerialPort port)
        {
            Device = device;
            Port = port;
            port.DataReceived += Port_DataReceived;
            Timer = new System.Timers.Timer();
            Timer.Elapsed += Timer_Elapsed;
            ARE = new AutoResetEvent(false);
        }

        public TaskBase(Device device, SerialPort port, Params needParam)
        {
            Device = device;
            Port = port;
            Parameter = needParam;
            port.DataReceived += Port_DataReceived;
            Timer = new System.Timers.Timer();
            Timer.Elapsed += Timer_Elapsed;
            ARE = new AutoResetEvent(false);
        }

        public List<byte> InData { get; set; } = null;

        public Device Device { get; private set; } = null;

        public Params Parameter { get; set; }

        public SerialPort Port { get; private set; } = null;

        private Task<Answer> InTask { get; set; } = null;

        private AutoResetEvent ARE { get; set; } = null;      

        private System.Timers.Timer Timer { get; set; } = null;

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ARE.Set();
            Timer.Stop();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (InData == null)
                return;
            Timer.Stop();
            int BTR = Port.BytesToRead;
            byte[] buf = new byte[BTR];
            Port.Read(buf, 0, BTR);
            InData.AddRange(buf);
            Timer.Interval = 100;
            Timer.Start();
        }

        public Answer Execute()
        {         
            return DoingMethod();
        }

        public void SendData(byte[] data)
        {
            InData = new List<byte>();
            Port.Write(data, 0, data.Length);
            Timer.Interval = 1000;
            Timer.Start();
            ARE.WaitOne();
        }

        public abstract Answer DoingMethod();

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Timer?.Dispose();
                    ARE?.Dispose();
                    InTask?.Dispose();
                    InData = null;
                    Port.DataReceived -= Port_DataReceived;
                }

                disposedValue = true;
            }
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // TODO: раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
