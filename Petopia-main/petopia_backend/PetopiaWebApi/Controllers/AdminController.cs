using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetopiaWebApi.Models;


namespace PetopiaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly PetopiaDbContext context;

        public AdminController(PetopiaDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Admin>>> GetAdmin()
        {
            var data = await context.Admins.ToListAsync(); // Corrected to context.Admins
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            var admin = await context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return admin;
        }
        //Create Admin
        //[HttpPost]
        //public async Task<ActionResult<Admin>> CreateAdmin(AdminDto adminDto)
        //{
        //    var admin = new Admin
        //    {
        //        Name = adminDto.Name,
        //        Phone = adminDto.Phone,
        //        Email = adminDto.Email
        //    };

        //    await context.Admins.AddAsync(admin); // Corrected to context.Admins
        //    await context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetAdminById), new { id = admin.AdminId }, admin);
        //}

        [HttpPut("{id}")]
        public async Task<ActionResult<Admin>> UpdateAdmin(int id, AdminDto adminDto)
        {
            if (id != adminDto.AdminId) // Corrected to adminDto.AdminId
            {
                return BadRequest();
            }

            var admin = await context.Admins.FindAsync(id); // Corrected to context.Admins
            if (admin == null)
            {
                return NotFound();
            }

            // Update admin properties
            admin.Name = adminDto.Name;
            admin.Phone = adminDto.Phone;
            admin.Email = adminDto.Email;

            context.Entry(admin).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            var admin = await context.Admins.FindAsync(id); // Corrected to context.Admins
            if (admin == null)
            {
                return NotFound();
            }
            context.Admins.Remove(admin); // Corrected to context.Admins
            await context.SaveChangesAsync();
            return Ok();
        }


        // POST: api/admin/register
        [HttpPost("register")]
        public async Task<ActionResult<Admin>> Register(AdminDto adminDto)
        {
            // Check if the admin already exists
            if (await context.Admins.AnyAsync(u => u.Email == adminDto.Email))
            {
                return BadRequest("Admin with this email already exists.");
            }

            var admin = new Admin
            {
                Name = adminDto.Name,
                Email = adminDto.Email,
                AdminId = adminDto.AdminId,
                Phone = adminDto.Phone,
            };

            await context.Admins.AddAsync(admin);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAdminById), new { id = admin.AdminId }, admin);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AdminLoginDto adminLoginDto)
        {
            var admin = await context.Admins.FirstOrDefaultAsync(u => u.Email == adminLoginDto.Email);
            if (admin == null || admin.AdminId != adminLoginDto.AdminId) // Compare plain text password
            {
                return Unauthorized("Invalid credentials.");
            }

            // Generate a token (you can implement JWT here)
            return Ok("Login successful.");
        }

        public class AdminDto
        {
            public int AdminId { get; set; }
            public string Name { get; set; } = null!;
            public string? Phone { get; set; }
            public string Email { get; set; } = null!;
        }

        public class AdminLoginDto
        {
            public int AdminId { get; set; }
            public string Email { get; set; } = null!;
        }
    }
}

