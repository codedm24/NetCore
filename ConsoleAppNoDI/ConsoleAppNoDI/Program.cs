using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppNoDI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            //without DI
            var controllerNoDI = new HomeControllerNoDI();
            string resultNoDI = controllerNoDI.Hello("Stephanie");
            Console.WriteLine($"Output No DI:{resultNoDI}");

            //using DI
            IGreetingService greetingService = new GreetingService();
            var controllerWithDI = new HomeController(greetingService);
            string resultWithDI = controllerWithDI.Hello("Stephanie");
            Console.WriteLine($"Output with DI: {resultWithDI}");

            //using DIContainer
            using (ServiceProvider container = RegisterServices())
            { 
                var controllerDIContainer = container.GetRequiredService<HomeController>();
                string resultDIContainer = controllerDIContainer.Hello("Katharina");
                Console.WriteLine($"Output with DI using ServiceProvider: {resultDIContainer}");
            }
        }

        static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddTransient<HomeController>();

            return services.BuildServiceProvider();
        }
    }
}
