using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InCore
{
    public class PortConfig : ICloneable
    {
        public string ComName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int Parity { get; set; }
        public int StopBits { get; set; }

        public object Clone()
        {
            return new PortConfig()
            {
                ComName = (string)ComName.Clone(),
                BaudRate = BaudRate,
                DataBits = DataBits,
                Parity = Parity,
                StopBits = StopBits
            };
        }
    }
}
