using Assignment2C2P.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Assignment2C2P.Models;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Assignment2C2P.Tests.Services
{
    public class CustomerServiceTest
    {
        private IList<Customer> customers;

        public CustomerServiceTest()
        {
            customers = new List<Customer>
            {
                new Customer {CustomerId = 1, Name = "Customer 1", Email = "customer1@mail.com", MobileNo = "0891234567"},
                new Customer {CustomerId = 2, Name = "Customer 2", Email = "customer2@mail.com", MobileNo = "0891234568"},
                new Customer {CustomerId = 3, Name = "Customer 3", Email = "customer3@mail.com", MobileNo = "0891234569"},
            };
        }

        [Fact]
        public void GetCustomerById_IdNotFound_MustReturnNull()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();

            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(customers);

            CustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var actual = service.GetCustomerById(0);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetCustomerById_ValidId_MustReturnCustomer()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();

            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(customers);

            CustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var actual = service.GetCustomerById(2);
            var expected = customers[1];

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
