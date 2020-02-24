using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInspector.Models
{
    public class FakeCounter : ICounterRepo
    {
        public IQueryable<CounterModels> CounterData => new List<CounterModels>
        {
            new CounterModels {index = 0, Current = 0.1, Voltage = 0.1, Frequency = 0.1, Power = 0.1, Descriptions  = "1", C_Power = new CounterModels.CountingPower {Month = 1, CountedPower = 1}},
            new CounterModels {index = 1, Current = 0.2, Voltage = 0.2, Frequency = 0.2, Power = 0.2, Descriptions  = "2", C_Power = new CounterModels.CountingPower {Month = 2, CountedPower = 2}},
            new CounterModels {index = 2, Current = 0.3, Voltage = 0.3, Frequency = 0.3, Power = 0.3, Descriptions  = "3", C_Power = new CounterModels.CountingPower {Month = 3, CountedPower = 3}}
        }.AsQueryable();
    }
}
