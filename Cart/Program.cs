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
            var r1 = new Role() { Name = "Покупатель"};
            var r2 = new Role() { Name = "Товаровед"};

            var u1 = new User() { Login = "A1ex", Name = "Alex" };
            u1.role.Add(r1);
            u1.role.Add(r2);
            var u2 = new User() { Login = "7Le8", Name = "Gleb" };
            u2.role.Add(r1);

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

            Product[] menu = new Product[9]
                {
                     p1,p2,p3,p4,p5,p6,
                     new Product() { Name = "Колбаса", Price = 158.00M },
                     new Product() { Name = "Сало", Price = 199.00M },
                     new Product() { Name = "Сыр", Price = 112.50M }
                };

            Controller controller = new Controller();
            controller.SaveRange<Product>(menu);

            controller.Save<User>(u1);
            controller.Save<User>(u2);
            controller.Save<Order>(o1);
            controller.Save<Order>(o2);

        }
    }
}
