using Assignment2C2P.Controllers;
using Assignment2C2P.Messages;
using Assignment2C2P.Models;
using Assignment2C2P.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Assignment2C2P.Tests.Controllers
{
    public class CustomersControllerTest
    {
        [Fact]
        public void Inquiry_WhenCalled_WithNullRequestParameter_MustReturnsBadRequest()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            CustomersController controller = new CustomersController(mockCustomerService.Object);

            //Act
            var actual = controller.Inquiry(null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithNullRequestParameter_MustReturnsErrorResponseMessage_NoInquiryCriteria()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            CustomersController controller = new CustomersController(mockCustomerService.Object);

            //Act
            var action = controller.Inquiry(null);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage {Message = "No inquiry criteria"};

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithEmptyRequestParameter_MustReturnsErrorResponseMessage_NoInquiryCriteria()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();
            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var criteria = new CustomerInquiryRequestMessage();

            //Act
            var action = controller.Inquiry(criteria);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage { Message = "No inquiry criteria" };

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndEmail_MustReturnsOkObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var criteria = new CustomerInquiryRequestMessage{CustomerID = validId.ToString(), Email = validEmail};

            //Act
            var action = controller.Inquiry(criteria);
            var actual = action.Result;

            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndEmail_MustReturnsCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer {CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567"};
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var criteria = new CustomerInquiryRequestMessage { CustomerID = validId.ToString(), Email = validEmail };

            //Act
            var action = controller.Inquiry(criteria);
            var result = action.Result as OkObjectResult;

            var actual = result?.Value as Customer;
            var expected = customer;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidIdAndValidEmail_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidId = 0;
            var criteria = new CustomerInquiryRequestMessage { CustomerID = invalidId.ToString(), Email = validEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndInvalidEmail_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidEmail = "invalid@mail.com";
            var criteria = new CustomerInquiryRequestMessage {CustomerID = validId.ToString(), Email = invalidEmail};

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundResult>(actual.Result);
        }

        //[Fact]
        //public void Inquiry_WhenCalled_WithJsonInvalidIdOnly_MustReturnsBadRequest()
        //{
        //    //Arrange
        //    var mockCustomerService = new Mock<ICustomerService>();
        //    CustomersController controller = new CustomersController(mockCustomerService.Object);

        //    var json = "{ \"customerID\":\"InvalidID\" }";
        //    var message = JsonConvert.DeserializeObject<CustomerInquiryRequestMessage>(json);

        //    //Act
        //    var actual = controller.Inquiry(message);

        //    //Assert
        //    Assert.IsType<BadRequestObjectResult>(actual.Result);
        //}

    }
}