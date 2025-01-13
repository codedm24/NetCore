using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    internal class ServiceA : IServiceA, IDisposable
    {
        private int _n;
        public ServiceA(INumberService numberService)
        {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ServiceA)}, {_n}");
        }

        public void A()
        { 
            Console.WriteLine($"{nameof(A)}, {_n}");
        }

        public void Dispose() {
            Console.WriteLine($"Disposing {nameof(ServiceA)}, {_n}");
        }
    }
}
