using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFood.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public long TotalPay { get; set; }
        public bool State { get; set; }
        public string Note { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}