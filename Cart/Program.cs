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

            var p1 = new Product() { Name = "Хлеб", Price = 34.50M };
            var p2 = new Product() { Name = "Молоко", Price = 59.90M };
            var p3 = new Product() { Name = "Соль", Price = 12.30M };
            var p4 = new Product() { Name = "Шоколад", Price = 75.50M };
            var p5 = new Product() { Name = "Кефир", Price = 47.30M };
            var p6 = new Product() { Name = "Масло сливочное", Price = 112.30M };

            var o1 = new Order() { User = u1 };
            o1.Add(p1, 2);
            o1.Add(p2, 1);
            o1.Add(p3, 1);

            var o2 = new Order() { User = u2 };
            o2.Add(p4, 2);
            o2.Add(p5, 1);


            using (AppContext db = new AppContext(options))
            {
                db.Products.AddRange(p1,p2,p3,p4,p5,p5);
                db.Users.AddRange(u1,u2);
                db.Orders.AddRange(o1,o2);
                db.SaveChanges();
            
                var res = db.Orders
                    .Include(o => o.User)
                    .Include(o => o.Products)
                    .Where(o => o.User.Name == "Gleb")
                    .ToList();

                var res2 = db.Products.OrderBy(o=> o.Name).ToList();

                var res3 = db.Products.Join(db.Positions,
                    pr => pr.Id,
                    po => po.Product.Id,
                    (pr, po) => new { pName = pr.Name, order = po.OrderId }
                    );

                foreach (var el in res3)
                {
                    Console.WriteLine(el.pName+" : "+el.order);
                }
            }
        }
    }
}
