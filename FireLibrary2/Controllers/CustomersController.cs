using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireLibrary2.DTOs;
using FireLibrary2.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FireLibrary2.Controllers
{
    [EnableCors]
    [Route("api/Customer")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IRepository repo, ILogger<CustomersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            List<CustomerDTO> result = new();
            /*
            try
            {
                result = await _repo.GetAllCustomersAsync();

                if (result == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }


            return result;
            */
            return NotFound();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            CustomerDTO result = new();
            /*
            try
            {
                result = await _repo.GetCustomerByIdAsync(id);

                if (result == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }
            Console.WriteLine("We are in Customer controller");
            Console.WriteLine(result.CustomerId);
            return Ok(result);
            */
            return NotFound();
        }

        //Get all orders for customer
        [HttpGet("Orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetCustomerOrders(int customerId)
        {
            List<OrderDTO> result = new();
            /*
            try
            {
                result = await _repo.GetCustomerOrdersASync(customerId);

                if (result == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok(result);
            */
            return NotFound();
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO request)
        {
            /*
            try
            {
                await _repo.UpdateCustomer(request);


            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok();
            */
            return StatusCode(500);
        }

    }
}

