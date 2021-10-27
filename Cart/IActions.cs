using Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    interface IActions
    {
        //Сохранение элементов
        public void Save<T>(object element);
        public void SaveRange<T>(object[] elements);

        //Установка ролей
        public void SetRole(User user, Role role);

        //Получение одного/списка элементов
        public T GetOneElement<T>(string filter);
        public List<T> GetAllElements<T>();

        //Сохранение заказа
        public void SaveOrder(Order order);

        //Удаление БД
        public void ClearDB();
    }
}
