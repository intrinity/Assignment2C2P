using Assignment2C2P.Models;

namespace Assignment2C2P.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        Customer GetCustomerByIdAndEmail(int id, string email);
    }
}