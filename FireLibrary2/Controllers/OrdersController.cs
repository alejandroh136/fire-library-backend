using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FireLibrary2.Models;
using FireLibrary2.DTOs;
using Microsoft.AspNetCore.Cors;

namespace FireLibrary2.Controllers
{
    [EnableCors]
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IRepository repo, ILogger<OrderController> logger)
        {
            _repo = repo;
            _logger = logger;
        }



        [HttpPost] //takes an order DTO, creates an order.
        public async Task<ActionResult> PostOrder(OrderDTO request)
        {

            try
            {
                string result = await _repo.PostOrderAsync(request);

                if (result == null)
                {
                    return Problem();
                }
                else
                {
                    switch (result)
                    {
                        case "toomany":
                            return BadRequest("Too many books on this order!");
                        case "duplicate":
                            return BadRequest("Please remove duplicate books!");
                        case "availability":
                            return BadRequest("One or more books is not available!");
                        case "success":
                            return Ok("Order placed!");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet] //gets a specific order
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            OrderDTO result = new();

            try
            {
                result = await _repo.GetOrderAsync(id);

                if (result == null)
                {
                    return BadRequest("Order not found!");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500);
            }

            return Ok(result);

        }

        [HttpPost("return/{id}")]
        public async Task<ActionResult> ReturnBooks(OrderDTO request)
        {
            try
            {
                await _repo.ReturnBooksAsync(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            //Returns 200OK along with returned book count. 
            return Ok($"You have returned {request.Books.Count()} books!");
        }

        [HttpPost("returnbook")]
        public async Task<ActionResult> ReturnOneBook(ReturnDTO bookToReturn)
        {
            try
            {
                await _repo.ReturnOneBookAsync(bookToReturn);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            //Returns 200OK along with returned book count. 
            return Ok("Book returned!");
        }

    }

}
