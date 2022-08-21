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
    public class BooksControllerTest
    {
        [Fact]
        public async Task GetBook_Input_Result()
        {
            BookDTO book = new BookDTO();
            book.Isbn = "12345";
            book.Pages = 200;
            book.TotalCopies = 5;
            book.AvalableCopies = 5;
            book.Genre = "Genre";

            Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);



            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookAsync("12345")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);


            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.GetBook("12345");

            var resultContent = result.Result as ContentResult;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }

            var item = result.Value;
            var contentResult = result as ActionResult<BookDTO>;

            Assert.Equal(book.Isbn, result.Value.Isbn);
        }

        [Fact]
        public async Task GetBook_BadInput_Notfound()
        {
            BookDTO book = new BookDTO();
            book.Isbn = "12345";
            book.Pages = 200;
            book.TotalCopies = 5;
            book.AvalableCopies = 5;
            book.Genre = "Genre";

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);



            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.GetBookAsync("1234555")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);


            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.GetBook("12345");

            var resultContent = result.Result as ContentResult;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }

            var item = result.Value;

            var contentResult = result as ActionResult<BookDTO>;
            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task SearchBooksGenre_Input_Result()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", Genre = "genre" });

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("genre")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.SearchBooksGenre("genre");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }
            Assert.Equal(1, resultContent.Count());
        }


        [Fact]
        public async Task SearchBooksGenre_BadInput_NoResult()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", Genre = "genre" });

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("genrrre")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.SearchBooksGenre("genre");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }
     
            Assert.IsType<NotFoundResult>(result.Result);     
        }

        [Fact]
        public async Task SearchBooksAuthor_Input_Result()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", AuthorName = "name", Genre = "genre" }); //new BookDTO { Isbn = "4321", AuthorName = "Name", Genre = "genre1"});

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("name")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.SearchBooksAuthor("name");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }
       
            Assert.Equal(1, resultContent.Count());
        }

        [Fact]
        public async Task SearchBooksAuthor_BadInput_NoResult()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", Genre = "genre", AuthorName = "name" });

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("Namee")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.SearchBooksAuthor("name");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task SearchBooksTitle_Input_Result()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", AuthorName = "name", Genre = "genre" , Title = "hello"}); //new BookDTO { Isbn = "4321", AuthorName = "Name", Genre = "genre1"});
            book.Add(new BookDTO { Isbn = "32142", Genre = "genre2", AuthorName = "name2", Title = "hello" });
            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("hello")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            var result = await cus.SearchBooksTitle("hello");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }
       
            Assert.Equal(2, resultContent.Count());
        }
        [Fact]
        public async Task SearchBookstTitle_BadInput_NoResult()
        {
            List<BookDTO> book = new List<BookDTO>();
            book.Add(new BookDTO { Isbn = "12345", Genre = "genre", AuthorName = "name", Title = "hello" });

            //Console.WriteLine("The Book test started");

            var ser = JsonSerializer.Serialize(book);

            Console.WriteLine(ser);

            Mock<ILogger<BooksController>> mockLogger = new();
            Mock<IRepository> mockRepo = new();

            mockRepo.Setup(repo => repo.SearchBooksGenreAsync("Hello")).ReturnsAsync(book);

            var cus = new BooksController(mockRepo.Object, mockLogger.Object);

            //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
            var result = await cus.SearchBooksTitle("hello");


            var resultContent = result.Value as List<BookDTO>;
            if (resultContent == null)
            {
                Console.WriteLine("Book Result Content is NULL");
            }
           
            Assert.IsType<NotFoundResult>(result.Result);
          
        }
        //[Fact]
        //public async Task GetBooks_Input_Result()
        //{
        //    List<BookDTO> book = new List<BookDTO>();
        //    book.Add(new BookDTO { Isbn = "12345", Genre = "genre" });

        //    //Console.WriteLine("The Book test started");

        //    var ser = JsonSerializer.Serialize(book);

        //    Console.WriteLine(ser);

        //    Mock<ILogger<BooksController>> mockLogger = new();
        //    Mock<IRepository> mockRepo = new();

        //    mockRepo.Setup(repo => repo.GetAllBooksAsync()).ReturnsAsync(book);

        //    var cus = new BooksController(mockRepo.Object, mockLogger.Object);

        //    //var actionResult = await cus.GetCustomer(12345) as OkObjectResult;
        //    var result = await cus.SearchBooksGenre("genre");


        //    var resultContent = result.Value as List<BookDTO>;
        //    if (resultContent == null)
        //    {
        //        Console.WriteLine("Book Result Content is NULL");
        //    }
        //    //var item = result.Value;

        //    //var contentResult = result as ActionResult<List<BookDTO>>;

        //    //var test = result.ToString() as CustomerDTO;
        //    //CustomerDTO cus1 = result.Value as CustomerDTO;

        //    //Assert.IsType<NotFoundObjectResult>(result.Result);
        //    //Assert.IsType<NotFoundResult>(result.Result);
        //    //Assert.Equal(book.Isbn, result.Value.Isbn);
        //    //Assert.Equal(1, resultContent.Count());
        //    Assert.IsType<BookDTO>(result.Result);
        //}


    }
}
