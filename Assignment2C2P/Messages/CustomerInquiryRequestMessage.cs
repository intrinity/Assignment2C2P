using System.ComponentModel.DataAnnotations;

namespace Assignment2C2P.Messages
{
    public class CustomerInquiryRequestMessage
    {
        public string CustomerID { get; set; }
        public string Email { get; set; }
    }
}