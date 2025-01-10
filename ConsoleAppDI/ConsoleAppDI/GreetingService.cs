using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNoDI
{
    //Without DI
    //internal class GreetingService
    //{
    //    public string Greet(string name) => $"Hello, {name}";
    //}

    //using DI
    internal class GreetingService : IGreetingService
    {
        public string Greet(string name) => $"Hello, {name}";
    }

}
