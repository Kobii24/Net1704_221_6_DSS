using DSS.Wpf.UI;
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

namespace DSS.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Open_wExtraDiamond_Click(object sender, RoutedEventArgs e)
        {
            var p = new wExtraDiamond();
            p.Owner = this;
            p.Show();
        }
        private async void Open_wDiamondShell_Click(object sender, RoutedEventArgs e)
        {
            var p = new wDiamondShell();
            p.Owner = this;
            p.Show();
        }
    }
}