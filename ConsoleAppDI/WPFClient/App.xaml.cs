using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterServices();
        }

        private void RegisterServices() {
            var services = new ServiceCollection();
            services.AddSingleton<IMessageService, WPFMessageService>();
            services.AddTransient<ShowMessageViewModel>();
            Container = services.BuildServiceProvider();
        }
    }


    

}
