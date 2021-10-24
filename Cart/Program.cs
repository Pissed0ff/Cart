using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Cart
{
    class Program
    {
        static void Main(string[] args)
        {
            #region createDbContextOptionsBuilder
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            #endregion

            var u1 = new User() { Login = "A1ex", Name = "Alex" };
            var u2 = new User() { Login = "7Le8", Name = "Gleb" };

            var product1 = new Product() { Name = "Хлеб", Price = 34.50M };
            var product2 = new Product() { Name = "Молоко", Price = 59.90M };
            var product3 = new Product() { Name = "Соль", Price = 12.30M };

            var O1 = new Order() { User = u1 };
            O1.Add(product1, 2);
            O1.Add(product2, 1);
            O1.Add(product3, 1);


            using (AppContext db = new AppContext(options))
            {
                db.Users.Add(u1);
                db.Users.Add(u2);
                db.Products.AddRange(product1, product2, product3);
                db.Orders.Add(O1);
                db.SaveChanges();
            }
        }
    }
}
