using Microsoft.AspNetCore.Mvc;
using Pedalacom.BLogic.Authentication;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly BasicAuthenticationHandler _authenticationHandler;

        public LoginController(BasicAuthenticationHandler authenticationHandler)
        {
            _authenticationHandler = authenticationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(User user)
        {
            try
            {
              

                var authenticateResult = await _authenticationHandler.AuthenticateUserAsync(HttpContext);

                if (authenticateResult.Succeeded)
                {
                    
                    return Ok();
                }
                else
                {
                    
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

    
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

