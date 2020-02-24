using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebInspector.Models;

namespace WebInspector.Controllers
{
    public class CounterController : Controller
    {
        private ICounterRepo Counter { get; set; }

        public CounterController (ICounterRepo counterRepo)
        {
            Counter = counterRepo;
        }

        public ViewResult List() => View(Counter.CounterData);
    }
}