﻿using Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    class ConsoleActions : IActions
    {
        public void ClearDB()
        {
            throw new NotImplementedException();
        }

        public void Save<T>(object element)
        {
            Console.WriteLine(element.ToString());
        }

        public void SaveRange<T>(object[] elements)
        {
            foreach(var el in elements)
                Console.WriteLine(el.ToString());
        }

        public void SetRole(User user, Role role)
        {
            throw new NotImplementedException();
        }
    }
}
