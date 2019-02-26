using Newtonsoft.Json.Linq;
using NJsonSchema.Validation.FormatValidators;

namespace Assignment2C2P.Validation
{
    public class InquiryCriteriaValidation
    {
        public static bool ValidateCustomerID(string customerIdCriteria)
        {
            if (string.IsNullOrEmpty(customerIdCriteria)) return false;

            //Max length 10 digits
            if (customerIdCriteria.Length > 10) return false;

            //Check if it could be parsed to number
            if (!int.TryParse(customerIdCriteria, out int customerId)) return false;

            //Check if customer id is valid
            return customerId > 0;
        }

        public static bool ValidateEmail(string email)
        {
            EmailFormatValidator validator = new EmailFormatValidator();
            return validator.IsValid(email, JTokenType.None);
        }
    }
}