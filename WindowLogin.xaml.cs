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
using System.Windows.Shapes;

namespace Appli
{
    /// <summary>
    /// Logique d'interaction pour WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window 
    {
        public WindowLogin()
        {
            InitializeComponent();
            //tbmdp.Password;
        }
        private void bt_add(object sender, RoutedEventArgs e)
        {
            if (tbid.Text != "" && tbmdp.Password != "")
            {
                CRUD_Login.getlogin(tbid.Text, tbmdp.Password);
                Close();
            }
            else
            {
                MessageBox.Show("Un des champs est vide");
            }
        }
    }
}
