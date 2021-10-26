using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    interface IActions
    {
        public void Save<T>(object element);
        public void SaveRange<T>(object[] elements);

        public void ClearDB();
    }
}
