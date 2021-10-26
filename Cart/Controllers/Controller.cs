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
        IActions actions = new DBActions();

        public void Save<T>(object elem)
        {
            actions.Save<T>(elem);
        }

        public void SaveRange<T>(object[] elems)
        {
            actions.SaveRange<T>(elems);
        }

        public void ClearDB()
        {
            actions.ClearDB();
        }

        public Controller()
        {}
    }
}
