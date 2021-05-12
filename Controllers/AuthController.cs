using InternAPI.Model;
using InternAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InternAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Auth auth)
        {
            var user = _authRepo.Authenticate(auth.Mail, auth.Password);
            if (user is null)
                return BadRequest(new { message = "Username or password is wrong!" });
            return Ok(user);
        }
    }
}