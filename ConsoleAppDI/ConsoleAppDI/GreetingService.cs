using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDI
{
    //Without DI
    internal class GreetingServiceNoDI
    {
        public string Greet(string name) => $"Hello, {name}";
    }

    //using DI
    internal class GreetingServiceDI : IGreetingService
    {
        public string Greet(string name) => $"Hello, {name}";
    }


    //using DI with options
    internal class GreetingServiceWithOptions : IGreetingService
    {
        private readonly string _from;
        public GreetingServiceWithOptions(IOptions<GreetingServiceOptions> options) => _from = options.Value.From;

            
        public string Greet(string name) => $"Hello, {name}! Greetings from {_from}";
    }


    internal static class GreetingServiceExtensions
    {
        public static IServiceCollection AddGreetingService(this IServiceCollection collection,
            Action<GreetingServiceOptions> setupAction)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (setupAction == null)
                throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            return collection.AddTransient<IGreetingService, GreetingServiceWithOptions>();
        }
    }

}
