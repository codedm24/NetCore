using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    //without DI
    //internal class HomeController
    //{
    //    public string Hello(string name) { 
    //        GreetingService greetingService = new GreetingService();
    //        return greetingService.Greet(name);
    //    }
    //}

    //using DI
    internal class HomeController
    { 
        private IGreetingService _greetingService;

        public HomeController(IGreetingService greetingService)
        { 
            _greetingService = greetingService ?? throw new ArgumentNullException(nameof(greetingService));
        }

        public string Hello(string name) => _greetingService.Greet(name);
    }
}
