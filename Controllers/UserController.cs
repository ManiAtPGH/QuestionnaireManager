using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string userRole = String.Empty;

            // Get the user details from the claims
            string userEmail = HttpContext.User.FindFirstValue("email");
            var roleClaim = User.FindFirst(ClaimTypes.Role);

            if (roleClaim != null)
            {
                userRole = roleClaim.Value;
            }

            if (userRole != null && userRole == "patient") return Unauthorized();

            var data = _userService.GetUsers();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var data = _userService.GetUser(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                if(data.UserId == null)
                {
                    return NotFound("Requested resource is not available");
                }
                return Ok(data);
            }
        }

        [HttpPost("GetResourceLocationWithId")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetCreatedResourceLocation(string id)
        {
            return $"https://localhost:44354/api/User/GetUser/{id}";
        }

        [HttpPost]
        public IActionResult Post([FromBody] BaseUserModel model)
        {
            if (model != null)
            {
                var roleClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                //if (roleClaim != null)
                //{
                //    model.CreatedById = new Guid(roleClaim.Value);
                //}
                var newUserId = _userService.AddUser(model);
                
                if (newUserId > 0)
                {
                    return StatusCode(201);
                }

                return StatusCode(500);
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] BaseUserModel model)
        {
            if (model != null)
            {
                var roleClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                //if (roleClaim != null)
                //{
                //    model.CreatedById = new Guid(roleClaim.Value);
                //}

                if(_userService.GetUser(id).UserId == null)
                {
                    return NotFound("Requested resource is not available");
                }

                var result = _userService.UpdateUser(model, id);

                if (result > 0)
                {
                    return StatusCode(200);
                }

                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (Guid.Empty == id)
            {
                return BadRequest();
            }
            else
            {
                if (_userService.GetUser(id).UserId == null)
                {
                    return NotFound("Requested resource is not available");
                }
                if (_userService.DeleteUser(id) > 0)
                {
                    return Ok(new { message = "User deleted successfully." });
                }
                return StatusCode(500, "Deleting User Failed");
            }
        }
    }
}
