using Cart.Controllers;
using Cart.Models;
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
            //var r1 = new Role() { Name = "Покупатель"};
            //var r2 = new Role() { Name = "Товаровед"};

            //var u1 = new User() { Login = "A1ex", Name = "Alex" };          
            //var u2 = new User() { Login = "7Le8", Name = "Gleb" };


            //var p1 = new Product() { Name = "Хлеб", Price = 34.50M };
            //var p2 = new Product() { Name = "Молоко", Price = 59.90M };
            //var p3 = new Product() { Name = "Соль", Price = 12.30M };
            //var p4 = new Product() { Name = "Шоколад", Price = 75.50M };
            //var p5 = new Product() { Name = "Кефир", Price = 47.30M };
            //var p6 = new Product() { Name = "Масло сливочное", Price = 112.30M };

            //Product[] menu = new Product[9]
            //    {
            //         p1,p2,p3,p4,p5,p6,
            //         new Product() { Name = "Колбаса", Price = 158.00M },
            //         new Product() { Name = "Сало", Price = 199.00M },
            //         new Product() { Name = "Сыр", Price = 112.50M }
            //    };

            Controller controller = new Controller();

            //controller.ClearDB();
            //controller.SaveRange<Product>(menu);
            //controller.Save<User>(u1);
            //controller.Save<User>(u2);
            //controller.Save<Role>(r2);
            //controller.Save<Role>(r1);

            //controller.SetRole(u1, r1);
            //controller.SetRole(u1, r2);
            //controller.SetRole(u2, r1);

            #region temporary query
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            var Options = optionsBuilder
                .UseSqlServer("Server=DESKTOP-NVMUHO2;Database=cart;Trusted_Connection=True;")
                .Options;
            using (var db = new AppContext(Options) )
            {
                var users = db.Users.Include(u => u.Roles).ToList();
                // выводим все курсы
                foreach (var user in users)
                {
                    Console.WriteLine($"User: {user.Name}");
                    // выводим всех студентов для данного кура
                    foreach (Role role in user.Roles)
                        Console.WriteLine(role.Name);
                    Console.WriteLine("-------------------");
                }

            }
            #endregion

        }
    }
}
