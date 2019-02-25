using System;
using System.Collections.Generic;

namespace Assignment2C2P.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Transaction = new HashSet<Transaction>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
