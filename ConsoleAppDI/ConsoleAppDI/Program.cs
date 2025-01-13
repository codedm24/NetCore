using ConsoleAppDI;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;

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

            SingletonAndTransient();

            UsingScoped();
        }

        static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IGreetingService, GreetingService>();
            services.AddTransient<HomeController>();

            return services.BuildServiceProvider();
        }

        private static void SingletonAndTransient()
        {
            Console.WriteLine(nameof(SingletonAndTransient));

            ServiceProvider RegisterServices1()
            {
                IServiceCollection services = new ServiceCollection();
                services.AddSingleton<IServiceA, ServiceA>();
                services.AddTransient<IServiceB, ServiceB>();
                services.AddTransient<ControllerX>();
                services.AddSingleton<INumberService, NumberService>();
                return services.BuildServiceProvider();
            }

            using (ServiceProvider serviceProvider = RegisterServices1()) { 
                ControllerX x1 = serviceProvider.GetRequiredService<ControllerX>();
                x1.M();
                x1.M();
                Console.WriteLine($"Requesting {nameof(ControllerX)}");
                ControllerX X2 = serviceProvider.GetRequiredService<ControllerX>();
                X2.M();

                Console.WriteLine();
            }
        }

        private static void UsingScoped()
        {
            Console.WriteLine(nameof(UsingScoped));

            ServiceProvider RegisterServices2() {
                var services = new ServiceCollection();
                services.AddSingleton<INumberService, NumberService>();
                services.AddScoped<IServiceA, ServiceA>();
                services.AddSingleton<IServiceB, ServiceB>();
                services.AddTransient<IServiceC, ServiceC>();
                return services.BuildServiceProvider();
            }

            using (var container = RegisterServices2()) {
                using (IServiceScope scope1 = container.CreateScope()) { 
                    IServiceA? a1 = scope1.ServiceProvider.GetService<IServiceA>();
                    a1?.A();
                    IServiceA? a2 = scope1.ServiceProvider.GetService<IServiceA>();
                    a2?.A();
                    IServiceB? b1 = scope1.ServiceProvider.GetService<IServiceB>();
                    b1?.B();
                    IServiceC? c1 = scope1.ServiceProvider.GetService<IServiceC>();
                    c1?.C();
                    IServiceC? c2= scope1.ServiceProvider.GetService<IServiceC>();
                    c2?.C();
                }
                Console.WriteLine("end of scope1");
                
                using (IServiceScope scope2 = container.CreateScope()) { 
                    IServiceA? a3 = scope2.ServiceProvider.GetService<IServiceA>();
                    a3?.A();
                    IServiceB? b2 = scope2.ServiceProvider.GetService<IServiceB>();
                    b2?.B();
                    IServiceC? c3 = scope2.ServiceProvider.GetService<IServiceC>();
                    c3?.C();
                }
                Console.WriteLine("end of scope2");
                Console.WriteLine();
            }
        }
    }
}
