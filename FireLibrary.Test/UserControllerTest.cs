//using Xunit;
//using Xunit.Sdk;
//using Moq;
//using System.Text.Json;
//using Microsoft.EntityFrameworkCore;
//using FireLibrary2.Models;
//using FireLibrary2.Controllers;
//using FireLibrary2.Data;
//using Microsoft.AspNetCore.Mvc;
//using FireLibrary2.DTOs;
//using Microsoft.Extensions.Logging;
//using Castle.Core.Resource;
//using Microsoft.Extensions.Configuration;
//using System.Configuration;

//namespace FireLibrary.Test
//{
//    public class UserControllerTest
//    {
//        [Fact]
//        public async Task GetUsers_Input_Result()
//        {
//            const string someScenario = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";
//            User user = new User();
//            user.Username = "username";
//            user.UserId = 12345;
//            user.PasswordHash = Convert.FromBase64String(someScenario);
//            user.PasswordSalt = Convert.FromBase64String(someScenario);



//            Console.WriteLine("The Book test started");

//            var ser = JsonSerializer.Serialize(user);

//            Console.WriteLine(ser);



//            var mockRepo = new Mock<DbSet<User>>();

//            mockRepo.Setup(repo => repo.FindAsync(12345)).ReturnsAsync(user);


//            Mock<DataContext> mockContext = new();
//            Mock<IConfiguration> mockIcon = new();
           
//            mockContext.Setup(a => a.Users).Returns(mockRepo.Object);
//            var cus = new UsersController(mockContext.Object, mockIcon.Object);


//            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
//            var result = await cus.GetUser (12345);

//            var resultContent = result.Result as ContentResult;
//            if (resultContent == null)
//            {
//                Console.WriteLine("Book Result Content is NULL");
//            }

//            var item = result.Value;
//            User user1 = item as User;

//            Assert.Equal(user, user1);

//            // var contentResult = result as ActionResult<OrderDTO>;

//            //var test = result.ToString() as CustomerDTO;
//            //CustomerDTO cus1 = result.Value as CustomerDTO;

//            //Assert.Equal(book.Isbn, result.Value.Isbn);
//            Assert.IsType<OkObjectResult>(result.Result);
//        }
//    }
//}

