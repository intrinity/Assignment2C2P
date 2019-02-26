using System;
using Assignment2C2P.Messages;
using Assignment2C2P.Models;
using Assignment2C2P.Services;
using Assignment2C2P.Validation;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

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
        [SwaggerResponse("200", typeof(OkObjectResult), Description = "Success")]
        [SwaggerResponse("404", typeof(NotFoundObjectResult), Description = "Inquiry criteria does not match any record on the database")]
        [SwaggerResponse("400", typeof(BadRequestObjectResult), Description = "Invalid request")]
        [SwaggerResponse("500", typeof(ObjectResult), Description = "An error occurred")]
        public ActionResult<Customer> Inquiry(CustomerInquiryRequestMessage criteria)
        {
            try
            {
                if (criteria == null || string.IsNullOrEmpty(criteria.CustomerID) && string.IsNullOrEmpty(criteria.Email))
                {
                    return BadRequest(new ErrorResponseMessage { Message = "No inquiry criteria" });
                }

                if (!string.IsNullOrEmpty(criteria.CustomerID) && !string.IsNullOrEmpty(criteria.Email))
                {
                    if (!InquiryCriteriaValidation.ValidateCustomerID(criteria.CustomerID)) return BadRequest(new ErrorResponseMessage { Message = "Invalid Customer ID" });
                    if (!InquiryCriteriaValidation.ValidateEmail(criteria.Email)) return BadRequest(new ErrorResponseMessage { Message = "Invalid Email" });

                    var customer = _customerService.GetCustomerByIdAndEmail(Convert.ToInt32(criteria.CustomerID), criteria.Email);
                    if (customer == null) return NotFound(null);

                    return Ok(customer);
                }

                if (!string.IsNullOrEmpty(criteria.CustomerID))
                {
                    if (!InquiryCriteriaValidation.ValidateCustomerID(criteria.CustomerID)) return BadRequest(new ErrorResponseMessage { Message = "Invalid Customer ID" });

                    var customer = _customerService.GetCustomerById(Convert.ToInt32(criteria.CustomerID));
                    if (customer == null) return NotFound(null);

                    return Ok(customer);
                }

                if (!string.IsNullOrEmpty(criteria.Email))
                {
                    if (!InquiryCriteriaValidation.ValidateEmail(criteria.Email)) return BadRequest(new ErrorResponseMessage { Message = "Invalid Email" });

                    var customer = _customerService.GetCustomerByEmail(criteria.Email);
                    if (customer == null) return NotFound(null);

                    return Ok(customer);
                }

                return BadRequest(null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseMessage {Message = "An error occured"});
            }
        }
    }
}