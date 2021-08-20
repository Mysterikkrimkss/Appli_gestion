using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { // ouvre page client
            WindowClient Main = new WindowClient();
            Main.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {   // ouvre page document
            WindowDocument Main = new WindowDocument();
            Main.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        { // ouvre page recette
            WindowRecette Main = new WindowRecette();
            Main.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        { // ouvre page recette
            WindowMission Main = new WindowMission();
            Main.Show();
        }
    }
}
