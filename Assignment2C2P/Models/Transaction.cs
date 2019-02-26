using System;
using Newtonsoft.Json;

namespace Assignment2C2P.Models
{
    public partial class Transaction
    {
        [JsonProperty("id")]
        public int TransactionId { get; set; }
        [JsonProperty("date")]
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        [JsonProperty("currency")]
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
