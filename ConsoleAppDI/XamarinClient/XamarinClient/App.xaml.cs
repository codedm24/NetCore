using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.Services;
using XamarinClient.Views;

namespace XamarinClient
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            RegisterServices();
            MainPage = new AppShell();
        }

        public IServiceProvider Container { get; set; }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IPageSerrvice, PageService>();
            serviceCollection.AddSingleton<IMessageService, XamarinMessageService>();
            serviceCollection.AddTransient<ShowMessageViewModel>();
            Container = serviceCollection.BuildServiceProvider();
        }

    }
}
