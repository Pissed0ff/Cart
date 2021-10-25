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
    public class Controller
    {
        DbContextOptions options;
        delegate void Del(object obj);
        public void Save<T>(object elem) 
        {
            //Создание начальных значений и запросы
            using (AppContext db = new AppContext(options))
            {
                Dictionary<Type, Del> dic = new Dictionary<Type, Del>()
                {
                    { typeof(User),(elem) => db.Users.Add((User)elem) }
                };

                dic[typeof(T)].Invoke(elem);
                

                db.SaveChanges();
            }
        }

        public Controller()
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
