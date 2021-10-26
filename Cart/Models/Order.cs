using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart
{
    public class Order
    {
        public int Id { get; set; }
        public string Cod { get; set; }
        public User User { get; set; }
        public List<Position> Products { get; set; }
        public DateTime DateOfOrder { get; set; }

        public void Add(Product prod, float quantity)
        {
            Products.Add(new Position() { Product = prod, Qantity = quantity });
        }
        public Order()
        {
            Products = new List<Position>();
        }
    }

}
