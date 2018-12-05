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

namespace StockV3
{

    public partial class MainWindow : Window
    {

        StockDbEntities db;

        public int Init
        {
            set
            {
                if (value == 1)
                {
                    editProduct.IsEnabled = addProduct.IsEnabled = providerForm.IsEnabled = articleForm.IsEnabled = logForm.IsEnabled = true;
                }
                else { skladForm.IsEnabled = logForm.IsEnabled = true; }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProductForm editor = new ProductForm();
            if (editor.ShowDialog() == true)
            {

            }
        }

        public class ProductClass
        {
            private StockDbEntities db;
            private int _id;
            private int _mincapacity;
            private int _maxcapacity;
            private int _typeId;

            private decimal _price;
            private decimal _capacity;

            private String _information;
            private String _units;
            private String _title;



            public ProductClass(int id, decimal capacity, int minc, int maxc, decimal price, String info, string units, int typeid, string title)
            {

                db = new StockDbEntities();
                _id = id; _capacity = capacity; _mincapacity = minc; _maxcapacity = maxc; _price = price;
                _information = info; _units = units; _typeId = typeid; _title = title;
            }

            public int Id => _id;
            public decimal Capacity => _capacity;
            public int Mincapacity => _mincapacity;
            public int Maxcapacity => _maxcapacity;
            public decimal Price => _price;
            public String Information => _information;
            public String Units => _units;
            public String Title => _title;

            public String Type => db.Type.ToList().Where(g => g.Id == _typeId).FirstOrDefault().Title;
        }

        List<ProductClass> PList = new List<ProductClass>();
        List<Product> pls = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
            db = new StockDbEntities();
            foreach (var item in db.Product.ToList())
                PList.Add(new ProductClass(item.Id, item.Capacity, item.MinCapacity, item.MaxCapacity, item.Price, item.Information, item.Units, item.TypeId, item.Title));


            Products.ItemsSource = PList;
        }



        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ProductForm editor = new ProductForm();
            if (editor.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ProviderForm form = new ProviderForm();
            if (form.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Article form = new Article();
            if (form.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Income form = new Income();
            if (form.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            LogsForm form = new LogsForm();
            if (form.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Login form = new Login(this);
            if (form.ShowDialog() == true)
            {

            }
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e) => Close();

        private void Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            int id = PList[Products.SelectedIndex].Id;
            Product P = db.Product.Where(i => i.Id == id).FirstOrDefault();

             foreach (Provider item in P.Provider.ToList())
            Providers.Items.Add(item.Title);
        }

        private void Providers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Numbers.Items.Clear();
            Types.Items.Clear();
            Provider p = db.Provider.ToList().Where(t => t.Title == Providers.SelectedValue.ToString()).FirstOrDefault();

            Contact.Text = p.ContactName;
            Mail.Text = p.ContactMail;
            foreach (var item in p.Type.ToList())
                Types.Items.Add(item.Title.ToString());
            foreach (var item in p.ContactNumber.ToList())
                Numbers.Items.Add(item.Number);
        }
    }
}
