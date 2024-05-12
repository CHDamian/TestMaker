using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestMaker.Views;

namespace TestMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Ustaw widok domyślny na Main
            Main_Click(null, null);
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            // Wczytaj widok Main
            MainContent.Content = new Main();
        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            // Wczytaj widok Editor
            MainContent.Content = new Editor();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            // Wczytaj widok Test
            MainContent.Content = new Test();
        }
    }
}
