using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Persistence.Repositories;

namespace RestService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IRepoUser repoUser, HelperJWT helperJWT) : ControllerBase
    {
        public readonly IRepoUser _repoUser = repoUser;
        private readonly HelperJWT _helperJWT = helperJWT;

        public class LoginRequest
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                var existingUser = await _repoUser.FindByUsername(login.Username);
                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }
                if (!BCrypt.Net.BCrypt.Verify(login.Password, existingUser.Password))
                {
                    return Unauthorized("Invalid password.");
                }
          
                var token = _helperJWT.GenerateToken(existingUser);
                return Ok(new { user = existingUser, token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
