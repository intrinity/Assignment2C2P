using System;
using System.Collections.Generic;

namespace Assignment2C2P.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
