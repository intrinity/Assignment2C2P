﻿using Assignment2C2P.Messages;
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

        [HttpPost]
        [Route("inquiry")]
        public ActionResult<Customer> Inquiry(CustomerInquiryRequestMessage criteria)
        {
            if (criteria == null || !criteria.CustomerID.HasValue && string.IsNullOrEmpty(criteria.Email))
            {
                return BadRequest(new ErrorResponseMessage {Message = "No inquiry criteria"});
            }

            return Ok();
        }
    }
}