using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("mobile")]
        public string MobileNo { get; set; }

        [JsonProperty("transactions")]
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
