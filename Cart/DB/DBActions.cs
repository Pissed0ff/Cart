using Cart.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    class DBActions : IActions
    {
        DbContextOptions Options;
        delegate void Del(object obj);
        delegate T Del1<T>(string folter);
        delegate object DelAll<T>();
        
        void SaveInTable<T>(AppContext db, object elem)
        {
            Dictionary<Type, Del> dic = new Dictionary<Type, Del>()
                {
                    { typeof(User),(elem) => db.Users.Add((User)elem) },
                    { typeof(Order),(elem) => db.Orders.Add((Order)elem) },
                    { typeof(Product),(elem) => db.Products.Add((Product)elem) },
                    { typeof(Role),(elem) => db.Roles.Add((Role)elem) }
                };

            dic[typeof(T)].Invoke(elem);
        }
        object GetFromTable<T>(AppContext db, string filter)
        {
            Dictionary<Type, Del1<Object>> dic = new Dictionary<Type, Del1<Object>>()
                {
                    { typeof(User),(filter) => db.Users.FirstOrDefault(u => u.Login == filter) },
                    { typeof(Product),(filter) => db.Products.FirstOrDefault(p => p.Name == filter) },
                    { typeof(Role),(filter) => db.Roles.FirstOrDefault(r => r.Name == filter) }
                };
            return dic[typeof(T)].Invoke(filter);
        }

        public void Save<T>(object elem)
        {
            //Создание начальных значений и запросы
            using (AppContext db = new AppContext(Options))
            {
                SaveInTable<T>(db, elem);
                db.SaveChanges();
            }
        }

        public void SaveRange<T>(object[] elems)
        {
            using (AppContext db = new AppContext(Options))
            {
                foreach (var el in elems)
                    SaveInTable<T>(db, el);
                db.SaveChanges();
            }
        }

        public void ClearDB()
        {
            using (AppContext db = new AppContext(Options))
            {
                db.Database.EnsureDeleted();
                db.SaveChanges();    
            }
        }

        public void SetRole(User _user, Role _role)
        {
            using (AppContext db = new AppContext(Options))
            {
                var user = db.Users.FirstOrDefault(u => u.Login == _user.Login);
                var role = db.Roles.FirstOrDefault(r => r.Name == _role.Name);
                if (user != null && role != null)
                {
                    user.Roles.Add(role);
                    db.SaveChanges();
                }
            }
        }
        public void SaveOrder(Order order)
        {
            using(var db = new AppContext(Options))
            {
                var _user = db.Users.FirstOrDefault(u => u == order.User);
                List<Position> positions =new List<Position>();
                
                //Сопоставление продуктов и сохранение их в БД
                foreach (var el in order.Products)
                {
                    var _position = new Position();
                    _position.Product = db.Products.FirstOrDefault(p => p == el.Product);
                    _position.Qantity = el.Qantity;
                    db.Positions.Add(_position);
                    positions.Add(_position);
                }
                Order _order = new Order()
                {
                    User = _user,
                    Products = positions
                };
                db.Orders.Add(_order);
                db.SaveChanges();
            }
        }

        public T GetOneElement<T>(string filter)
        {
            using(var db = new AppContext(Options))
            {
                return (T)GetFromTable<T>(db, filter);
            }
        }
        public List<T> GetAllElements<T>()
        {
            using (var db = new AppContext(Options))
            {
                Dictionary<Type, DelAll<Object>> dic = new Dictionary<Type, DelAll<Object>>()
                {
                    { typeof(User),() => db.Users.ToList() },
                    { typeof(Product),() => db.Products.ToList() },
                    { typeof(Role),() => db.Roles.ToList() }
                };
                return (List<T>)dic[typeof(T)].Invoke();
            }
        }

        public DBActions()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            var str = Directory.GetCurrentDirectory();
            builder.SetBasePath(str+"\\DB");
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            Options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
        }
    }
}
