using Assignment2C2P.Validation;
using Xunit;

namespace Assignment2C2P.Tests.Validation
{
    public class InquiryCriteriaValidationTest
    {
        [Fact]
        public void ValidateCustomerID_LengthWasExceed_ReturnsFalse()
        {
            //Arrange
            string invalidCustomerId = "12345678901";

            //Act
            var actual = InquiryCriteriaValidation.ValidateCustomerID(invalidCustomerId);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateCustomerID_NoneNumericCriteria_ReturnsFalse()
        {
            //Arrange
            string invalidCustomerId = "ID1234";

            //Act
            var actual = InquiryCriteriaValidation.ValidateCustomerID(invalidCustomerId);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateCustomerID_ZeroCustomerIdCriteria_ReturnsFalse()
        {
            //Arrange
            string invalidCustomerId = "-1";

            //Act
            var actual = InquiryCriteriaValidation.ValidateCustomerID(invalidCustomerId);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateCustomerID_CustomerIdCriteriaLessThanZero_ReturnsFalse()
        {
            //Arrange
            string invalidCustomerId = "-1";

            //Act
            var actual = InquiryCriteriaValidation.ValidateCustomerID(invalidCustomerId);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateCustomerID_ValidCriteria_ReturnsTrue()
        {
            //Arrange
            string validCustomerId = "123456";

            //Act
            var actual = InquiryCriteriaValidation.ValidateCustomerID(validCustomerId);

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void ValidateEmail_InvalidEmail_ReturnsFalse()
        {
            //Arrange
            string invalidEmail = "invalid@email";

            //Act
            var actual = InquiryCriteriaValidation.ValidateEmail(invalidEmail);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void ValidateEmail_ValidEmail_ReturnsTrue()
        {
            //Arrange
            string validEmail = "valid@email.com";

            //Act
            var actual = InquiryCriteriaValidation.ValidateEmail(validEmail);

            //Assert
            Assert.True(actual);
        }
    }
}