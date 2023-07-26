using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
