using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNoDI
{
    internal class HomeControllerNoDI
    {
        public string Hello(string name)
        {
            GreetingService greetingService = new GreetingService();
            return greetingService.Greet(name);
        }
    }
}
