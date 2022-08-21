using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FireLibrary2.DTOs;
using FireLibrary2.Models;
using Microsoft.AspNetCore.Cors;

namespace FireLibrary2.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IRepository repo, ILogger<BooksController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {

            List<BookDTO> result = new();

            /*try
            {
                result = await _repo.GetAllBooksAsync();

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
            return NotFound()
        }

        // GET: api/Books/5
        [HttpGet("search/isbn")]
        public async Task<ActionResult<BookDTO>> GetBook(string isbn)
        {
            BookDTO result = new();
            /*
            try
            {
                result = await _repo.GetBookAsync(isbn);

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
            Console.WriteLine("We are in Books Controller");
            return result;*/
            return NotFound();
        }

        [HttpGet("search/genre")]
        public async Task<ActionResult<List<BookDTO>>> SearchBooksGenre(string genre)
        {
            List<BookDTO> result = new();
            /*
            try
            {
                result = await _repo.SearchBooksGenreAsync(genre.ToLower());

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

        [HttpGet("search/author")]
        public async Task<ActionResult<List<BookDTO>>> SearchBooksAuthor(string author)
        {
            List<BookDTO> result = new List<BookDTO>();
            /*
            try
            {
                result = await _repo.SearchBooksAuthorAsync(author.ToLower());

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

        [HttpGet("search/title")]
        public async Task<ActionResult<List<BookDTO>>> SearchBooksTitle(string title)
        {
            List<BookDTO> result = new List<BookDTO>();
            /*
            try
            {
                result = await _repo.SearchBooksTitleAsync(title.ToLower());

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


    }
}
