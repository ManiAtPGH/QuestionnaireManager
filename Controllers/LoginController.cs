using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _login;

        public LoginController(ILoginService login)
        {
            _login = login;
        }

        [HttpGet]
        public IActionResult Get(string userName, string password)
        {
            var loginCheck = _login.Login(userName, password);
            if (loginCheck != null)
            {
                return Ok(loginCheck);
            }
            else { return StatusCode(401); }

        }
    }
}
