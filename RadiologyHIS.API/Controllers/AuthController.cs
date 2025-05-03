using Microsoft.AspNetCore.Mvc;
using RadiologyHIS.API.Data.Services;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService; // to use the methods

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            var result = await _authService.SignUpAsync(signUpRequest);
            if (!result)
                return BadRequest(new { message = "Registration failed. Email might already be used." });

            return Ok(new { message = "Registration successful!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _authService.LoginAsync(loginRequest);
            if (response == null)
                return Unauthorized("Invalid credentials.");

            return Ok(response);
        }
    }
}
