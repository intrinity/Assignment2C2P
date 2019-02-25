using Assignment2C2P.Controllers;
using Assignment2C2P.Messages;
using Assignment2C2P.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Assignment2C2P.Tests.Controllers
{
    public class CustomersControllerTest
    {
        [Fact]
        public void Inquiry_WithNullRequestParameter_MustReturnsBadRequest()
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
        public void Inquiry_WithNullRequestParameter_MustReturnsErrorResponseMessage_NoInquiryCriteria()
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
            Assert.Equal(expected.Message, actual.Message);
        }
    }
}