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

        public double Voltage { get; set; }

        #endregion
    }
}
