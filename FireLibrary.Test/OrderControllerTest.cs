using Xunit;
using Xunit.Sdk;
using Moq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using FireLibrary2.Models;
using FireLibrary2.Controllers;
using FireLibrary2.Data;
using Microsoft.AspNetCore.Mvc;
using FireLibrary2.DTOs;
using Microsoft.Extensions.Logging;

namespace FireLibrary.Test
{
    public class OrderControllerTest
    {
        [Fact]
        public async Task GetOrder_Input_Result()
        {
            OrderDTO order = new OrderDTO();
            order.orderId = 12345;
            order.CustomerId = 123456;
            order.DateLent = DateTime.Now;
            order.DateDue = DateTime.Now;

            Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(order);

            Console.WriteLine(ser);



            Mock<ILogger<OrderController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.GetOrderAsync(12345)).ReturnsAsync(order);

            var cus = new OrderController(mockRepo.Object, mockLogger.Object);


            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.GetOrder(12345);

            var resultContent = result.Result as ContentResult;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }

            var item = result.Value;

            var contentResult = result as ActionResult<OrderDTO>;

            //var test = result.ToString() as CustomerDTO;
            //CustomerDTO cus1 = result.Value as CustomerDTO;

            //Assert.Equal(book.Isbn, result.Value.Isbn);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetOrder_BadInput_NotFound()
        {
            OrderDTO order = new OrderDTO();
            order.orderId = 12345;
            order.CustomerId = 123456;
            order.DateLent = DateTime.Now;
            order.DateDue = DateTime.Now;

            Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(order);

            Console.WriteLine(ser);



            Mock<ILogger<OrderController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.GetOrderAsync(1234555)).ReturnsAsync(order);

            var cus = new OrderController(mockRepo.Object, mockLogger.Object);


            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.GetOrder(12345);

            var resultContent = result.Result as ContentResult;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }

            var item = result.Value;

            var contentResult = result as ActionResult<OrderDTO>;

            //var test = result.ToString() as CustomerDTO;
            //CustomerDTO cus1 = result.Value as CustomerDTO;

            //Assert.Equal(book.Isbn, result.Value.Isbn);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}

