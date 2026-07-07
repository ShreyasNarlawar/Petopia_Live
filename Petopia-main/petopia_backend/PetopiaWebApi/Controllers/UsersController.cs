using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaWebApi.Models;

namespace PetopiaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PetopiaDbContext context;

        public UsersController(PetopiaDbContext context)
        {
            this.context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var data = await context.Users.ToListAsync();
            return Ok(data);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST: api/users/ Create Users
        //[HttpPost]
        //public async Task<ActionResult<User>> CreateUser(UserDto userDto)
        //{
        //    var user = new User
        //    {
        //        Name = userDto.Name,
        //        Email = userDto.Email,
        //        Password = userDto.Password, // Hash the password
        //        PhoneNo = userDto.PhoneNo,
        //        Location = userDto.Location,
        //        UserRole = userDto.UserRole
        //    };

        //    await context.Users.AddAsync(user);
        //    await context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        //}

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserDto userDto)
        {
            if (id != userDto.UserId) // Assuming UserDto has UserId property
            {
                return BadRequest();
            }

            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password; // Hash the new password
            user.PhoneNo = userDto.PhoneNo;
            user.Location = userDto.Location;
            user.UserRole = userDto.UserRole;

            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return Ok();
        }


        // POST: api/users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto userDto)
        {
            // Check if the user already exists
            if (await context.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                return BadRequest("User  with this email already exists.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password, // Store plain text password
                PhoneNo = userDto.PhoneNo,
                Location = userDto.Location,
                UserRole = userDto.UserRole
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }


        // POST: api/users/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto userLoginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user == null || user.Password != userLoginDto.Password) // Compare plain text password
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate a token (you can implement JWT here)
            return Ok("Login successful.");
        }


        //private string HashPassword(string password)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return Convert.ToBase64String(hashedPassword);
        //    }
        //}

        //private bool VerifyPassword(string password, string hashedPassword)
        //{
        //    using (var hmac = new HMACSHA512(Convert.FromBase64String(hashedPassword)))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        return Convert.ToBase64String(computedHash) == hashedPassword;
        //    }
        //}



        public class UserDto
        {
            public int UserId { get; set; }

            [Required(ErrorMessage = "Name is required.")]
            public string Name { get; set; } = null!;

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Password is required.")]
            [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
            [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[\W_]).+$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
            public string Password { get; set; } = null!;

            [Required(ErrorMessage = "Phone number is required.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
            public string PhoneNo { get; set; } = null!;

            public string? Location { get; set; }

            [Required(ErrorMessage = "User  role is required.")]
            public string UserRole { get; set; } = null!;

        }

        public class UserLoginDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}