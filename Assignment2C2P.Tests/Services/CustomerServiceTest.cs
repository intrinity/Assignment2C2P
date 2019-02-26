using Assignment2C2P.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Assignment2C2P.Models;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace Assignment2C2P.Tests.Services
{
    public class CustomerServiceTest
    {
        private readonly IList<Customer> _customers;
        private readonly IList<Transaction> _transactions;

        public CustomerServiceTest()
        {
            _customers = new List<Customer>
            {
                new Customer {CustomerId = 1, Name = "Customer 1", Email = "customer1@mail.com", MobileNo = "0891234567"},
                new Customer {CustomerId = 2, Name = "Customer 2", Email = "customer2@mail.com", MobileNo = "0891234568"},
                new Customer {CustomerId = 3, Name = "Customer 3", Email = "customer3@mail.com", MobileNo = "0891234569"}
            };

            _transactions = new List<Transaction>
            {
                new Transaction {TransactionId = 1, TransactionDate = DateTime.Now, Amount = 1234.56m, CurrencyCode = "THB", Status = "Success", CustomerId = 2},
                new Transaction {TransactionId = 2, TransactionDate = DateTime.Now, Amount = 100.12m, CurrencyCode = "THB", Status = "Success", CustomerId = 3},
                new Transaction {TransactionId = 3, TransactionDate = DateTime.Now, Amount = 0.47m, CurrencyCode = "USD", Status = "Failed", CustomerId = 3},
                new Transaction {TransactionId = 4, TransactionDate = DateTime.Now, Amount = 223.45m, CurrencyCode = "USD", Status = "Canceled", CustomerId = 3}
            };
        }

        [Fact]
        public void GetCustomerById_IdNotFound_MustReturnNull()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var idNotInDatabase = 0;
            var actual = service.GetCustomerById(idNotInDatabase);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetCustomerById_ValidId_MustReturnCustomer()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validId = 2;
            var actual = service.GetCustomerById(validId);
            var expected = _customers[1];

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomerById_ValidId_MustReturnCustomerRecentTransactions()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validId = 3;
            var customer = service.GetCustomerById(validId);

            var actualTransactionCount = customer.Transaction.Count;
            var expectedTransactionCount = 3;

            //Assert
            Assert.Equal(expectedTransactionCount, actualTransactionCount);
        }

        [Fact]
        public void GetCustomerByEmail_EmailNotFound_MustReturnNull()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var emailNotInDatabase = "customer_not_found@mail.com";
            var actual = service.GetCustomerByEmail(emailNotInDatabase);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetCustomerByEmail_ValidEmail_MustReturnCustomer()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validEmail = "customer3@mail.com";
            var actual = service.GetCustomerByEmail(validEmail);
            var expected = _customers[2];

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomerByEmail_ValidEmail_MustReturnCustomerRecentTransactions()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validEmail = "customer3@mail.com";
            var customer = service.GetCustomerByEmail(validEmail);

            var actualTransactionCount = customer.Transaction.Count;
            var expectedTransactionCount = 3;

            //Assert
            Assert.Equal(expectedTransactionCount, actualTransactionCount);
        }

        [Fact]
        public void GetCustomerByIdAndEmail_IdNotFound_And_ValidEmail_MustReturnNull()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var idNotInDatabase = 5;
            var validEmail = "customer1@mail.com";
            var actual = service.GetCustomerByIdAndEmail(idNotInDatabase, validEmail);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetCustomerByIdAndEmail_ValidId_And_EmailNotFound_MustReturnNull()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validId = 2;
            var emailNotInDatabase = "customer_not_found@mail.com";
            var actual = service.GetCustomerByIdAndEmail(validId, emailNotInDatabase);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetCustomerByIdAndEmail_ValidIdAndEmail_MustReturnCustomer()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validId = 1;
            var validEmail = "customer1@mail.com";
            var actual = service.GetCustomerByIdAndEmail(validId, validEmail);
            var expected = _customers[0];

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCustomerByIdAndEmail_ValidIdAndEmail_MustReturnCustomerRecentTransactions()
        {
            //Arrange
            var dbContextMock = new Mock<Assignment2C2PContext>();
            dbContextMock.Setup(x => x.Customer).ReturnsDbSet(_customers);
            dbContextMock.Setup(x => x.Transaction).ReturnsDbSet(_transactions);

            ICustomerService service = new CustomerService(dbContextMock.Object);

            //Act
            var validId = 3;
            var validEmail = "customer3@mail.com";
            var customer = service.GetCustomerByIdAndEmail(validId, validEmail);

            var actualTransactionCount = customer.Transaction.Count;
            var expectedTransactionCount = 3;

            //Assert
            Assert.Equal(expectedTransactionCount, actualTransactionCount);
        }
    }
}
