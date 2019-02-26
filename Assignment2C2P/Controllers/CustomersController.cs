using System;
using Assignment2C2P.Messages;
using Assignment2C2P.Models;
using Assignment2C2P.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2C2P.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Inquiry customer detail and recent transactions
        /// </summary>
        /// <param name="criteria">Customer ID and email address</param>
        /// <returns>Customer detail and recent transactions</returns>
        [HttpPost]
        [Route("inquiry")]
        public ActionResult<Customer> Inquiry(CustomerInquiryRequestMessage criteria)
        {
            if (criteria == null || string.IsNullOrEmpty(criteria.CustomerID) && string.IsNullOrEmpty(criteria.Email))
            {
                return BadRequest(new ErrorResponseMessage {Message = "No inquiry criteria"});
            }

            if (!string.IsNullOrEmpty(criteria.CustomerID) && !string.IsNullOrEmpty(criteria.Email))
            {
                var customer = _customerService.GetCustomerByIdAndEmail(Convert.ToInt32(criteria.CustomerID), criteria.Email);
                if (customer == null) return NotFound(null);

                return Ok(customer);
            }

            if (!string.IsNullOrEmpty(criteria.CustomerID))
            {
                var customer = _customerService.GetCustomerById(Convert.ToInt32(criteria.CustomerID));
                if (customer == null) return NotFound(null);

                return Ok(customer);
            }

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                var customer = _customerService.GetCustomerByEmail(criteria.Email);
                if (customer == null) return NotFound(null);

                return Ok(customer);
            }

            return BadRequest(null);
        }
    }
}