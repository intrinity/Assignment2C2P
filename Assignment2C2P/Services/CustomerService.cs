using System.Linq;
using Assignment2C2P.Models;

namespace Assignment2C2P.Services
{
    public class CustomerService
    {
        private readonly Assignment2C2PContext _db;

        public CustomerService(Assignment2C2PContext db)
        {
            _db = db;
        }

        public Customer GetCustomerById(int id)
        {
            return _db.Customer.FirstOrDefault(c => c.CustomerId == id);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _db.Customer.FirstOrDefault(c => c.Email == email);
        }
    }
}