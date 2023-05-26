using Microsoft.AspNetCore.Mvc;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserResponseController : ControllerBase
    {
        private readonly IUserResponseService _userResponseService;

        public UserResponseController(IUserResponseService userResponseService)
        {
            _userResponseService = userResponseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _userResponseService.GetUsersResponses();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserResponse([FromQuery] string UserId)
        {
            var data = _userResponseService.GetUserResponses(UserId);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{userId}/{userResponseId}")]
        public IActionResult GetUserResponse([FromQuery] string userId, [FromQuery] string userResponseId)
        {
            var data = _userResponseService.GetUserResponse(userResponseId);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }


        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetCreatedResourceLocation(string id)
        {
            return $"https://localhost:44354/api/UserResponse/GetUserResponse/{id}";
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserResponseModel value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            else
            {
                string UserResponseId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
                return CreatedAtAction("GetCreatedResourceLocation", new { id = UserResponseId }, value);
            }
        }

    }
}
