// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PakaianApi.Data;
using PakaianApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Periksa apakah username sudah ada di database
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return BadRequest("Username sudah terdaftar.");
            }

            if (user.Role != UserRole.Admin && user.Role != UserRole.Customer)
            {
                return BadRequest("Role tidak valid. Hanya Admin dan Customer yang diperbolehkan.");
            }

            // Tambahkan user baru ke database. Id akan di-generate otomatis.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("Registrasi berhasil.");
        }

        // Helper method untuk mendapatkan role user dari database
        public static async Task<(bool success, UserRole role)> TryGetUserRoleAsync(ApplicationDbContext context, string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                return (true, user.Role);
            }
            return (false, UserRole.Customer);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // Cari user di database berdasarkan username dan password
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (existingUser != null)
            {
                // Anda bisa juga mengembalikan Id di sini jika diinginkan
                return Ok(new { Message = "Login berhasil", Role = existingUser.Role, UserId = existingUser.Id });
            }

            return Unauthorized("Username atau password salah.");
        }

        // Endpoint untuk debugging
        [HttpGet("all-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}