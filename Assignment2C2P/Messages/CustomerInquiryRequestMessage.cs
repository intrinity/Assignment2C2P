using System.ComponentModel.DataAnnotations;

namespace Assignment2C2P.Messages
{
    /// <summary>
    /// Inquiry criteria
    /// </summary>
    public class CustomerInquiryRequestMessage
    {
        /// <summary>
        /// ID of customer
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// Email address of customer
        /// </summary>
        public string Email { get; set; }
    }
}