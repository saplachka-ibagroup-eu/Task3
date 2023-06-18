using System.Windows;
using Task3.UI;

namespace Task3
{
      public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }
    }
}
