using DISampleLib;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinClient.Services;

namespace XamarinClient.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            IServiceProvider container = (Application.Current as App).Container;
            ViewModel = (ShowMessageViewModel)container.GetService(typeof(ShowMessageViewModel));
            ((IPageSerrvice)container.GetService(typeof(IPageSerrvice))).Page = this;
            this.BindingContext = ViewModel;
        }

        public ShowMessageViewModel ViewModel { get; }
    }
}