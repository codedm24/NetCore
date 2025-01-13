using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    internal class ServiceB : IServiceB, IDisposable
    {
        private int _n;
        public ServiceB(INumberService numberService) {
            _n = numberService.GetNumber();
            Console.WriteLine($"ctor {nameof(ServiceB)}, {_n}");
        }

        public void B() { 
            Console.WriteLine($"{nameof(B)}, {_n}");
        }

        public void Dispose() { 
            Console.WriteLine($"Disposing {nameof(ServiceB)}, {_n}");
        }
    }
}
