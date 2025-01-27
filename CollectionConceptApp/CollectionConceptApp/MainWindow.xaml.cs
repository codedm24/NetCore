using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollectionConceptApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LinkedListViewModel LinkedListViewModel1;
        public MainWindow()
        {
            InitializeComponent();
            LinkedListViewModel1 = new LinkedListViewModel();
            this.DataContext = this;
        }

        private void btnCollectionType_Click(object sender, RoutedEventArgs e)
        {
            Button? clicked = sender as Button;
            switch (clicked!.Name)
            {
                case "btnList":
                    TabControl1.SelectedIndex = 0;
                    break;
                case "btnQueue":
                    TabControl1.SelectedIndex = 1;
                    break;
                case "btnStack":
                    TabControl1.SelectedIndex = 2;
                    break;
                case "btnLinkedList":
                    TabControl1.SelectedIndex = 3;
                    break;
                default:
                    TabControl1.SelectedIndex = 0;
                    break;
            }
        }
    }
}