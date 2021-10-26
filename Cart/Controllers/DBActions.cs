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
        DbContextOptions options;
        delegate void Del(object obj);

        void SaveInTable<T>(AppContext db, object elem)
        {
            Dictionary<Type, Del> dic = new Dictionary<Type, Del>()
                {
                    { typeof(User),(elem) => db.Users.Add((User)elem) },
                    { typeof(Order),(elem) => db.Orders.Add((Order)elem) },
                    { typeof(Product),(elem) => db.Products.Add((Product)elem) }
                };

            dic[typeof(T)].Invoke(elem);
        }

        public void Save<T>(object elem)
        {
            //Создание начальных значений и запросы
            using (AppContext db = new AppContext(options))
            {
                SaveInTable<T>(db, elem);
                db.SaveChanges();
            }
        }

        public void SaveRange<T>(object[] elems)
        {
            //Создание начальных значений и запросы
            using (AppContext db = new AppContext(options))
            {
                foreach (var el in elems)
                    SaveInTable<T>(db, el);
                db.SaveChanges();
            }
        }

        public DBActions()
        {
            #region createDbContextOptionsBuilder
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            var str = Directory.GetCurrentDirectory();
            builder.SetBasePath(str);
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            #endregion
        }
    }
}
