using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockV3
{
    class DBManager
    {
        protected static StockDbEntities db = new StockDbEntities();
        public static int LogIn(string login, string password)
        {
            var user = db.User.Where(u => u.Login == login).FirstOrDefault();
            return (user != null && user.Password == password) ? user.Id : 0;

        }
        public static void AddArticle(string name)
        {
            db.Type.Add(new Type { Title = name });
            db.SaveChanges();
        }
        public static void ChangeArticle(string nameOld, string nameNew)
        {
            var type = db.Type.Where(u => u.Title == nameOld).FirstOrDefault();
            type.Title = nameNew;
            db.SaveChanges();
        }
        public static void DelArticle(string name)
        {
            var type = db.Type.Where(u => u.Title == name).FirstOrDefault();
            db.Type.Remove(type);
            db.SaveChanges();
        }
        public static void AddProvider(string title, string contactName, string email)
        {
            db.Provider.Add(new Provider { Title = title, ContactName = contactName, ContactMail = email });
            db.SaveChanges();
        }
        public static void ChangeProvider(string oldTitle, string title, string contactName, string email)
        {
            var provider = db.Provider.Where(u => u.Title == oldTitle).FirstOrDefault();
            provider.Title = title;
            provider.ContactMail = email;
            provider.ContactName = contactName;
            db.SaveChanges();
        }
        public static void DelProvider(string title)
        {
            var provider = db.Provider.Where(u => u.Title == title).FirstOrDefault();
            db.Provider.Remove(provider);
            db.SaveChanges();
        }
        public static void AddTypeToProvider(string provider, string type)
        {
            var p = db.Provider.Where(u => u.Title == provider).FirstOrDefault();
            var t = db.Type.Where(u => u.Title == type).FirstOrDefault();
            p.Type.Add(t);
            db.SaveChanges();
        }
        public static void DelTypeFromProvider(string provider, string type)
        {
            var p = db.Provider.Where(u => u.Title == provider).FirstOrDefault();
            var t = db.Type.Where(u => u.Title == type).FirstOrDefault();
            p.Type.Remove(t);
            db.SaveChanges();
        }
        public static void AddTelephone(string provider, string telephone)
        {
            var p = db.Provider.Where(u => u.Title == provider).FirstOrDefault();
            p.ContactNumber.Add(new ContactNumber { Number = telephone });
            db.SaveChanges();
        }
        public static void DelTelephone(string telephone)
        {
            var tel = db.ContactNumber.Where(u => u.Number == telephone).FirstOrDefault();
            db.ContactNumber.Remove(tel);
            db.SaveChanges();
        }
        public static void ChangeTelephone(string oldTel, string newTel)
        {
            var tel = db.ContactNumber.Where(u => u.Number == oldTel).FirstOrDefault();
            tel.Number = newTel;
            db.SaveChanges();
        }
        public static void AddProduct(string title, string units, int min, int max, decimal price, string info, string type)
        {
            var t = db.Type.Where(u => u.Title == type).FirstOrDefault();
            db.Product.Add(new Product { Title = title, Units = units, MinCapacity = min, MaxCapacity = max, Capacity = 0, Information = info, Price = price, TypeId = t.Id });
            db.SaveChanges();
        }
        public static void ChangeProduct(Product product, string title, string units, int min, int max, decimal price, string info, string type)
        {
            var t = db.Type.Where(u => u.Title == type).FirstOrDefault();
            product.Title = title;
            product.Units = units;
            product.MinCapacity = min;
            product.MaxCapacity = max;
            product.Price = price;
            product.Information = info;
            product.Type = t;
            db.SaveChanges();
        }
        public static Product ProductByTitle(string title)
        {
            var product = db.Product.Where(u => u.Title == title).FirstOrDefault();
            return product;
        }
        public static void ChangeCapacity(string product, decimal capacity, string comment)
        {
            var p = db.Product.Where(u => u.Title == product).FirstOrDefault();
            p.Capacity += capacity;
            db.Log.Add(new Log { Product = p, Capacity = capacity, Name = comment, Date = DateTime.Now });
            db.SaveChanges();
        }
    }
}
