using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    internal class ServiceC : IServiceC, IDisposable
    {
        private int _n;
        public ServiceC(INumberService numberService)
        {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ServiceC)}, {_n}");
        }

        public void C() { 
            Console.WriteLine($"{nameof(C)}, {_n}");
        }

        public void Dispose() { 
            Console.WriteLine($"Disposing {nameof(ServiceC)}, {_n}");
        }
    }
}
