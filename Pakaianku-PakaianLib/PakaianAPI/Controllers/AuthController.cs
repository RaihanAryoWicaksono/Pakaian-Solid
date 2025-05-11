//AuthController.cs Dimas
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PakaianApi.Models;
using System.Collections.Generic;

namespace PakaianApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Username → User
        private static Dictionary<string, User> _users = new();

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_users.ContainsKey(user.Username))
                return BadRequest("Username sudah terdaftar.");

            _users[user.Username] = user;
            return Ok("Registrasi berhasil.");
        }

        public static bool TryGetUserRole(string username, out UserRole role)
        {
            role = UserRole.Customer;
            if (_users.TryGetValue(username, out var user))
            {
                role = user.Role;
                return true;
            }
            return false;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (_users.TryGetValue(user.Username, out User existing) && existing.Password == user.Password)
            {
                return Ok(new { Message = "Login berhasil", Role = existing.Role });
            }

            return Unauthorized("Username atau password salah.");
        }

        // Endpoint untuk debugging
        [HttpGet("all-users")]
        public IActionResult GetUsers() => Ok(_users.Values);
    }
}
