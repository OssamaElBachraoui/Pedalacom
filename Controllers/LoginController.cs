using Microsoft.AspNetCore.Mvc;
using Pedalacom.BLogic.Authentication;

namespace Pedalacom.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult Auth(User user) {

            return Ok(User);
        }
    }

    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
