using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAdress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
    }
}
