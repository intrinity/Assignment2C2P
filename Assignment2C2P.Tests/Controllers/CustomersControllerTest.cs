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
        public void Inquiry_WhenCalled__WithNotFoundIdAndValidEmail_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var notFoundId = 10;
            var criteria = new CustomerInquiryRequestMessage { CustomerID = notFoundId.ToString(), Email = validEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidIdAndValidEmail_MustReturnsBadRequestObjectResult()
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
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidIdAndValidEmail_MustReturnsErrorResponseMessage_InvalidCustomerID()
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
            var action = controller.Inquiry(criteria);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage { Message = "Invalid Customer ID" };

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndNotFoundEmail_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var notFoundEmail = "invalid@mail.com";
            var criteria = new CustomerInquiryRequestMessage {CustomerID = validId.ToString(), Email = notFoundEmail};

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndInvalidEmail_MustReturnsBadRequestObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidEmail = "invalid@email";
            var criteria = new CustomerInquiryRequestMessage { CustomerID = validId.ToString(), Email = invalidEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdAndInvalidEmail_MustReturnsErrorResponseMessage_InvalidEmail()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidEmail = "invalid@email";
            var criteria = new CustomerInquiryRequestMessage { CustomerID = validId.ToString(), Email = invalidEmail };

            //Act
            var action = controller.Inquiry(criteria);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage { Message = "Invalid Email" };

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithNotFoundIdOnly_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var notFoundId = 10;
            mockCustomerService.Setup(s => s.GetCustomerById(notFoundId)).Returns(() => null);

            CustomersController controller = new CustomersController(mockCustomerService.Object);
            var criteria = new CustomerInquiryRequestMessage { CustomerID = notFoundId.ToString() };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidIdOnly_MustReturnsBadRequestObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerById(validId)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidId = 0;
            var criteria = new CustomerInquiryRequestMessage {CustomerID = invalidId.ToString()};

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidIdOnly_MustReturnsErrorResponseMessage_InvalidCustomerID()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerById(validId)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidId = 0;
            var criteria = new CustomerInquiryRequestMessage { CustomerID = invalidId.ToString() };

            //Act
            var action = controller.Inquiry(criteria);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage { Message = "Invalid Customer ID" };

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdOnly_MustReturnsOkObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };

            mockCustomerService.Setup(s => s.GetCustomerById(validId)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);
            var criteria = new CustomerInquiryRequestMessage { CustomerID = validId.ToString() };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidIdOnly_MustReturnsCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };    
            mockCustomerService.Setup(s => s.GetCustomerById(validId)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var criteria = new CustomerInquiryRequestMessage { CustomerID = validId.ToString() };

            //Act
            var action = controller.Inquiry(criteria);
            var result = action.Result as OkObjectResult;

            var actual = result?.Value as Customer;
            var expected = customer;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithNotFoundEmailOnly_MustReturnsNotFound()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var notFoundEmail = "notfound@mail.com";
            mockCustomerService.Setup(s => s.GetCustomerByEmail(notFoundEmail)).Returns(() => null);

            CustomersController controller = new CustomersController(mockCustomerService.Object);
            var criteria = new CustomerInquiryRequestMessage { Email = notFoundEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidEmail_MustReturnsBadRequestObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidEmail = "invalid@email";
            var criteria = new CustomerInquiryRequestMessage { Email = invalidEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithInvalidEmail_MustReturnsErrorResponseMessage_InvalidEmail()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByIdAndEmail(validId, validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var invalidEmail = "invalid@email";
            var criteria = new CustomerInquiryRequestMessage { Email = invalidEmail };

            //Act
            var action = controller.Inquiry(criteria);
            var badRequestResult = action.Result as BadRequestObjectResult;

            var actual = badRequestResult?.Value as ErrorResponseMessage;
            var expected = new ErrorResponseMessage { Message = "Invalid Email" };

            //Assert
            Assert.Equal(expected.Message, actual?.Message);
        }


        [Fact]
        public void Inquiry_WhenCalled__WithValidEmailOnly_MustReturnsOkObjectResult()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };

            mockCustomerService.Setup(s => s.GetCustomerByEmail(validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);
            var criteria = new CustomerInquiryRequestMessage { Email = validEmail };

            //Act
            var actual = controller.Inquiry(criteria);

            //Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public void Inquiry_WhenCalled__WithValidEmailOnly_MustReturnsCustomer()
        {
            //Arrange
            var mockCustomerService = new Mock<ICustomerService>();

            var validId = 1;
            var validEmail = "customer1@mail.com";
            var customer = new Customer { CustomerId = validId, Name = "Customer 1", Email = validEmail, MobileNo = "0891234567" };
            mockCustomerService.Setup(s => s.GetCustomerByEmail(validEmail)).Returns(customer);

            CustomersController controller = new CustomersController(mockCustomerService.Object);

            var criteria = new CustomerInquiryRequestMessage { Email = validEmail };

            //Act
            var action = controller.Inquiry(criteria);
            var result = action.Result as OkObjectResult;

            var actual = result?.Value as Customer;
            var expected = customer;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}