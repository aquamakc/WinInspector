using System;
using System.Collections.Generic;
using System.Text;

namespace InCore
{
    public class Device
    {
        static readonly double Epsylon = Math.Pow(2, -53);

        public delegate void ChangePropertyDelegate(DevProperties property, double value);
        public event ChangePropertyDelegate ChangePropertyEvent;
        public enum DevProperties : byte {Voltage, Current, Power, Frequency }

        #region Статичные свойства

        /// <summary>
        /// Скорость обмена по интерфейсу
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// Адрес прибора
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Пароль доступа
        /// </summary>
        public string Password { get; set; }

        public string Name { get; set; }

        public bool IsEconomyTraffic { get; set; } = true;

        #endregion

        #region Показания

        private double _voltage, _current, _power, _frequency;

        public double Voltage 
        {
            get { return _voltage; }
            set 
            {
                if (Math.Abs(_voltage - value) <= Epsylon && IsEconomyTraffic)
                    return;
                _voltage = value;
                ChangePropertyEvent?.Invoke(DevProperties.Voltage, value);
            }
        }

        public double Current
        {
            get { return _current; }
            set
            {
                if (Math.Abs(_current - value) <= Epsylon && IsEconomyTraffic)
                    return;
                _current = value;
                ChangePropertyEvent?.Invoke(DevProperties.Current, value);
            }
        }

        public double Power
        {
            get { return _power; }
            set
            {
                if (Math.Abs(_power - value) <= Epsylon && IsEconomyTraffic)
                    return;
                _power = value;
                ChangePropertyEvent?.Invoke(DevProperties.Power, value);
            }
        }

        public double Frequency
        {
            get { return _frequency; }
            set
            {
                if (Math.Abs(_frequency - value) <= Epsylon && IsEconomyTraffic)
                    return;
                _frequency = value;
                ChangePropertyEvent?.Invoke(DevProperties.Frequency, value);
            }
        }

        #endregion
    }
}
