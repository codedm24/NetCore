using System.ComponentModel;
using Xamarin.Forms;
using XamarinClient.ViewModels;

namespace XamarinClient.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}