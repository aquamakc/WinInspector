using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInspector.Models
{
    interface ICounterRepo
    {
        IQueryable<CounterModels> CounterData { get; }
    }
}
