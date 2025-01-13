using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    internal class NumberService : INumberService
    {
        private int _number = 0;
        public int GetNumber()
        { 
            return Interlocked.Increment(ref _number);
        }
    }
}
