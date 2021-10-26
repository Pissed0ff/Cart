using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    public class CartController
    {
        IActions actions = new DBActions();
        public List<Product> products;
        public List<Product> GetProducts()
        {
            return actions.GetAllElements<Product>();
        }
        public void CreateOrder()
        {
            
        }

        public CartController()
        {
            products = GetProducts();
        }


    }
}
