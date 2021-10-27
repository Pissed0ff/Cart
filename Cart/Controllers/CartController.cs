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
        public User User { get; set; }
        public Order Order { get; set; } = new Order();
        public List<Product> products;


        public void AddProductToOrder(Product product, float quantity)
        {
            Order.Add(product, quantity);
        }
        public List<Product> GetProducts()
        {
            return actions.GetAllElements<Product>();
        }

        public void SaveOrder()
        {
            if( User != null )
            {
                Order.User = User;
                actions.SaveOrder(Order);
            }       
        }

        public CartController()
        {
            products = GetProducts();
        }


    }
}
