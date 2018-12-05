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

namespace StockV3
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow form;
        public Login(MainWindow f)
        {
            InitializeComponent();
            form = f;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int res = DBManager.LogIn(loginBox.Text, passwordBox.Password);
            if (res == 0) { MessageBox.Show("Не верный логин или пароль"); }
            else
            {
                form.Init = res;
                Close();
            }
        }
    }
}
