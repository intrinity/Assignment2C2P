using System.Collections.Generic;
using System.Linq;
using Assignment2C2P.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2C2P.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Assignment2C2PContext _db;

        public CustomerService(Assignment2C2PContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        public Customer GetCustomerById(int id)
        {
            var customer = _db.Customer.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                var transactions = _db.Transaction.Where(t => t.CustomerId == customer.CustomerId).OrderByDescending(t=>t.TransactionId).Take(5);
                customer.Transaction = transactions.ToList();
            }

            return customer;
        }

        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="email">Email address of customer</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        public Customer GetCustomerByEmail(string email)
        {
            var customer = _db.Customer.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                var transactions = _db.Transaction.Where(t => t.CustomerId == customer.CustomerId).OrderByDescending(t => t.TransactionId).Take(5);
                customer.Transaction = transactions.ToList();
            }

            return customer;
        }

        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="email">Email address of customer</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        public Customer GetCustomerByIdAndEmail(int id, string email)
        {
            var customer = _db.Customer.FirstOrDefault(c => c.CustomerId == id && c.Email == email);
            if (customer != null)
            {
                var transactions = _db.Transaction.Where(t => t.CustomerId == customer.CustomerId).OrderByDescending(t => t.TransactionId).Take(5);
                customer.Transaction = transactions.ToList();
            }

            return customer;
        }
    }
}