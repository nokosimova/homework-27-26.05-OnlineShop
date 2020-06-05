using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public decimal? purchaseSum { get; set; }
        public string customerAdress { get; set; }
        public string customerTelephone { get; set; }
        public string delivetyTime { get; set; }
    }
}
