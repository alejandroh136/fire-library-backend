using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FireLibrary2.Data;
using FireLibrary2.Models;
using System.Security.Cryptography;
using FireLibrary2.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;

namespace FireLibrary2.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            /*
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
            */
            return NotFound();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            /*
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
            */
            return NotFound();
        }


        // POST: api/UsersControllerTest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDTO request)
        {
            /*
            if (_context.Users == null)
            {
                return Problem("Entity set 'DataContext.Users'  is null.");
            }

            List<User> existingUser = await _context.Users.ToListAsync();

            foreach (User u in existingUser)
            {
                if (request.Username == u.Username)
                {
                    return Conflict("Username taken, please try again!");
                }
            }

            User user = new User();
            Customer customer = new Customer();
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = request.Username;

            customer.Username = user.Username;

            _context.Customers.Add(customer);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return Ok("User and customer profiles created!");
            */
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDTO>> Login(UserDTO request)
        {
            LoginDTO result = new();
            /*
            if (_context.Users == null)
            {
                return Problem("Entity set 'DataContext.Users' is null.");
            }

            if (!UserExists(request.Username))
            {
                return Unauthorized("User not found!");
            }

            var user = await _context.Users.FirstAsync(acc => acc.Username == request.Username);

            if (!VerifyPasswordHash(user, request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Wrong password!");
            }

            Customer tmpCust = await _context.Customers.FirstAsync(cust => cust.Username == request.Username);

            result.CustomerId = tmpCust.CustomerId;
            result.Token = CreateToken(user);

            //return token + customerId + time for token to live as LoginDTO object
            return Ok(result);
            */
            return Ok();
        }





        //Dirty methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool UserExists(string username)
        {
            return (_context.Users?.Any(e => e.Username == username)).GetValueOrDefault();
        }


        private bool VerifyPasswordHash(User user, string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes((string)password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
