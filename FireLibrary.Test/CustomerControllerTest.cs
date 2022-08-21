using Xunit;
using Xunit.Sdk;
using Moq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using FireLibrary2.Models;
using FireLibrary2.Controllers;
using FireLibrary2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FireLibrary2.DTOs;
using System.Net;
using Castle.Core.Resource;
//using System.Web.Http.Results; 

namespace FireLibrary.Test;

public class CustomerControllerTest
{
    [Fact]
    public async Task GetCustomer_Input_Result()
    {
        CustomerDTO customer = new CustomerDTO();
        customer.BookCount = 150;
        customer.Canborrow = true;
        customer.CustomerId = 12345;
        customer.Username = "username";
        customer.Fines = 20;

        Console.WriteLine("The test started");

        var ser = JsonSerializer.Serialize(customer);

        Console.WriteLine(ser);



        Mock<ILogger<CustomersController>> mockLogger = new();
        Mock<IRepository> mockRepo = new();

        mockRepo.Setup(repo => repo.GetCustomerByIdAsync(12345)).ReturnsAsync(customer);

        var cus = new CustomersController(mockRepo.Object, mockLogger.Object);


        //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
        var result = await cus.GetCustomer(12345);

        var resultContent = result.Result as ContentResult;
        if (resultContent == null)
        {
            Console.WriteLine("Result Content is NULL");
        }

        var item = result.Value;

        var contentResult = result as ActionResult<CustomerDTO>;

        //var test = result.ToString() as CustomerDTO;
        //CustomerDTO cus1 = result.Value as CustomerDTO;

        //Assert.Equal(customer.CustomerId, result.Value.CustomerId);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomer_BadInput_NotFound()
    { 
        CustomerDTO customer = new CustomerDTO();
        customer.BookCount = 150;
        customer.Canborrow = true;
        customer.CustomerId = 12345;
        customer.Username = "username";
        customer.Fines = 20;

        Console.WriteLine("The test started");

        var ser = JsonSerializer.Serialize(customer);

        Console.WriteLine(ser);



        Mock<ILogger<CustomersController>> mockLogger = new();
        Mock<IRepository> mockRepo = new();

        mockRepo.Setup(repo => repo.GetCustomerByIdAsync(123445)).ReturnsAsync(customer);

        var cus = new CustomersController(mockRepo.Object, mockLogger.Object);


        //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
        var result = await cus.GetCustomer(12345);

        var resultContent = result.Result as ContentResult;
        if (resultContent == null)
        {
            Console.WriteLine("Result Content is NULL");
        }

        var item = result.Value;

        var contentResult = result as ActionResult<CustomerDTO>;

        //var test = result.ToString() as CustomerDTO;
        //CustomerDTO cus1 = result.Value as CustomerDTO;

        //Assert.Equal(customer.CustomerId, result.Value.CustomerId);
        Assert.IsType<NotFoundResult>(result.Result);
     }
 }


    //Assert.IsType<ActionResult<CustomerDTO>>(result);

    //Assert.IsType<OkObjectResult>(result);
    //CustomerDTO result1 = CustomerDTO.CreateCustomerDTO(customer);


    //Customer customer1 = item as Customer;
    //Assert.Equal(ser, resultContent.Content );


    //    Mock<DataContext> mockContext = new();
    //    mockContext.Setup(a => a.Customers).Returns(mockRepo.Object);

    //    var cus = new CustomersController(mockContext.Object);

    //    var result = await cus.GetCustomer(12345);

    //    var resultContent = (ActionResult)result.Result;

    //    if(resultContent == null)
    //    {
    //        Console.WriteLine("Result Content is NULL");
    //    }
    //    var item = result.Value;
    //    Customer customer1 = item as Customer;

    //    Assert.Equal(customer, customer1);

    //*********************************************************************************************************************
    //[Fact]
    //public async Task PostCustomer_Input_Result()
    //{
    //    Customer customer = new Customer();
    //    customer.BookCount = 150;
    //    customer.CanBorrow = true;
    //    customer.CustomerId = 12345;
    //    customer.Username = "username";
    //    customer.Fines = 20;

    //    var ser = JsonSerializer.Serialize(customer);

    //    System.Console.WriteLine(ser);

    //    //string jsonShouldbe = @"{""CustomerId"":12345,""Username"":""username"",""CanBorrow"":true,""Fines"":20,""BookCount"":150,""Orders"":null}";
    //    //Console.WriteLine(jsonShouldbe);

    //    var mockRepo = new Mock<DbSet<Customer>>();

    //    mockRepo.Setup(repo => repo.Add(customer));


    //    Mock<DataContext> mockContext = new();
    //    mockContext.Setup(a => a.Customers).Returns(mockRepo.Object);
    //    var cus = new CustomersController(mockContext.Object);

    //    var result = await cus.PostCustomer(customer);

    //    var resultContent = (ActionResult)result.Result;

    //    if (resultContent == null)
    //    {
    //        Console.WriteLine("Result Content is NULL");
    //    }
    //    var item = result.Value;
    //    Customer customer1 = item as Customer;

    //    Assert.Equal(customer, customer1);
    //}
    //*********************************************************************************************************************
    //[Fact]
    //public async Task PutCustomer_Input_Result()
    //{
    //    Customer customer = new Customer();
    //    customer.BookCount = 150;
    //    customer.CanBorrow = true;
    //    customer.CustomerId = 12345;
    //    customer.Username = "username";
    //    customer.Fines = 20;

    //    var ser = JsonSerializer.Serialize(customer);

    //    System.Console.WriteLine(ser);

    //    //string jsonShouldbe = @"{""CustomerId"":12345,""Username"":""username"",""CanBorrow"":true,""Fines"":20,""BookCount"":150,""Orders"":null}";
    //    //Console.WriteLine(jsonShouldbe);

    //    var mockRepo = new Mock<DbSet<Customer>>();
    //    mockRepo.Setup(b => b.SetModified(It.IsAny()));

    //    mockRepo.Setup(repo => repo.Add(customer));


    //    Mock<DataContext> mockContext = new();
    //    mockContext.Setup(a => a.Customers).Returns(mockRepo.Object);
    //    var cus = new CustomersController(mockContext.Object);

    //    var result = await cus.PutCustomer(12345, customer);

    //    //var resultContent = (ActionResult)result.Result;

    //    //if (resultContent == null)
    //    //{
    //    //    Console.WriteLine("Result Content is NULL");
    //    //}
    //    //var item = result.Value;
    //    //Customer customer1 = item as Customer;

    //    Assert.IsType<NoContentResult>(result);
    //}
