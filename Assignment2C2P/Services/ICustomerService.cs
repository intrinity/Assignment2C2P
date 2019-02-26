using Assignment2C2P.Models;

namespace Assignment2C2P.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        Customer GetCustomerById(int id);

        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="email">Email address of customer</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        Customer GetCustomerByEmail(string email);

        /// <summary>
        /// Get customer detail
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="email">Email address of customer</param>
        /// <returns>Customer detail and latest 5 transactions</returns>
        Customer GetCustomerByIdAndEmail(int id, string email);
    }
}