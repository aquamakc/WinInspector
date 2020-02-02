using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInspector.Models
{
    public class CounterModels
    {
        public int? index { get; set; }
        public string Descriptions { get; set; }
        public double Voltage { get; set; }
        public double Current { get; set; }    
        public double Frequency { get; set; }
        public double Power { get; set; }
        public CountingPower C_Power { get; set; } = null;
        
        public class CountingPower
        {
            public byte Month { get; set; }
            public double CountedPower { get; set; }
        }
    }
}
