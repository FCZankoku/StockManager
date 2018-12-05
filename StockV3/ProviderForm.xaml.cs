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
    /// Interaction logic for ProviderForm.xaml
    /// </summary>
    public partial class ProviderForm : Window
    {
        StockDbEntities db = new StockDbEntities();
        public ProviderForm()
        {
            InitializeComponent();
            ProviderListBox.ItemsSource = db.Provider.ToList();
        }

        private void ProviderListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PhoneList.ItemsSource = db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactNumber.ToList();
            email.Text = db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactMail;
            contact.Text = db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactName;
            name.Text = db.Provider.ToList()[ProviderListBox.SelectedIndex].Title;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(addTel.Text))
            {
                ContactNumber n = new ContactNumber()
                {
                    Number = addTel.Text,
                    ProviderId = db.Provider.ToList()[ProviderListBox.SelectedIndex].Id,
                    Provider = db.Provider.ToList()[ProviderListBox.SelectedIndex]
                };
                db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactNumber.Add(n); 
                PhoneList.ItemsSource = db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactNumber;
            }
            else
                MessageBox.Show("Вы не ввели номер телефона!");
        }

        private void addTel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactNumber.ToList().RemoveAt(db.Provider.ToList()[ProviderListBox.SelectedIndex].ContactNumber.Count - 1);
            PhoneList.Items.Refresh();
        }
    }
}
