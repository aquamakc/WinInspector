using System;
using System.Collections.Generic;
using System.Text;

namespace InCore
{
    public class Device
    {
        #region Статичные свойства

        /// <summary>
        /// Скорость обмена по интерфейсу
        /// </summary>
        public String Speed { get; set; }

        /// <summary>
        /// Адрес прибора
        /// </summary>
        public String Adress { get; set; }

        /// <summary>
        /// Пароль доступа
        /// </summary>
        public String Password { get; set; }

        public String Name { get; set; }

        #endregion

        #region Показания

        private double _voltage, _current, _power, _frequency;

        public event Action ParamChanges;

        public double Voltage 
        {
            get { return _voltage; }
            set { _voltage = value; ParamChanges?.Invoke(); }
        }

        public double Current
        {
            get { return _current; }
            set { _current = value; ParamChanges?.Invoke(); }
        }

        public double Power
        {
            get { return _power; }
            set { _power = value; ParamChanges?.Invoke(); }
        }

        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; ParamChanges?.Invoke(); }
        }

        #endregion
    }
}
