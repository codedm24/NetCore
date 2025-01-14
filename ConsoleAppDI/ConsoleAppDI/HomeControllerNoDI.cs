using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    internal class HomeControllerNoDI
    {
        public string Hello(string name)
        {
            GreetingServiceNoDI greetingService = new GreetingServiceNoDI();
            return greetingService.Greet(name);
        }
    }
}
