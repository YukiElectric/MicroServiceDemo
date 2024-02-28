using AccountAPI.Models;
using AccountAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository account;

        public AccountController(IAccountRepository repository)
        {
            account = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await account.SignUpAsync(signUpModel);
            return result.Succeeded ? Ok(new
            {
                status = true,
                message = "Register successfully"
            }) : BadRequest(new
            {
                status = false,
                message = "Register fail"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await account.SignInAsync(signInModel);

            return string.IsNullOrEmpty(result) ? Unauthorized(new
            {
                status = false,
                message = "Login fail, email or password isn't correct"
            }) : Ok(new
            {
                status = true,
                message = "Login successfully",
                token = result.ToString()
            });
        }

        [HttpGet("authen")]
        [Authorize]
        public async Task<IActionResult> Authentication()
        {
            return Ok(new
            {
                status = true,
                message = "You have student permission"
            });
        }

        [HttpGet("author")]
        [Authorize("admin")]
        public async Task<IActionResult> Authorization()
        {
            return Ok(new
            {
                status = true,
                message = "You have admin permission"
            });
        }
    }
}
