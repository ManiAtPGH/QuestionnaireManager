using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    public class UserQuestionnaireController : ControllerBase
    {
        private readonly IUserQuestionnaireService _userQuestionnaireService;

        public UserQuestionnaireController(IUserQuestionnaireService userQuestionnaireService)
        {
            _userQuestionnaireService = userQuestionnaireService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _userQuestionnaireService.GetUsersQuestionnaires();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{userId}/questionnaire")]
        public IActionResult GetUserQuestionnaires(Guid userId)
        {
            var data = _userQuestionnaireService.GetUserQuestionnaires(userId);
            if (data == null || data.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpGet("{userQuestionnaireId}")]
        public IActionResult GetUserQuestionnaire(Guid userQuestionnaireId)
        {
            var data = _userQuestionnaireService.GetUserQuestionnaire(userQuestionnaireId);
            if (data == null || data.QuestionnaireId == Guid.Empty)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BasicUserQuestionnaireModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            else
            {
                var result = _userQuestionnaireService.AddUserQuestionnaire(model);
                if (result > 0)
                {
                    return Ok("Added User Questionnaire Successfully");
                }
               return StatusCode(500, "Adding User Questionnaire Failed");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] BasicUserQuestionnaireModel model)
        {
            if (model == null || id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                var data = _userQuestionnaireService.GetUserQuestionnaire(id);
                if (data.UserQuestionnaireId == Guid.Empty)
                {
                    return NotFound("Requested resource is not available");
                }

                var result = _userQuestionnaireService.UpdateUserQuestionnaire(model, id);

                if (result > 0)
                {
                    return StatusCode(200);
                }

                return StatusCode(500);

            }
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
                if (_userQuestionnaireService.GetUserQuestionnaire(id).UserQuestionnaireId == Guid.Empty)
                {
                    return NotFound("Requested resource is not available");
                }
                if (_userQuestionnaireService.DeleteUserQuestionnaire(id) > 0)
                {
                    return Ok(new { message = "User Questionnaire deleted successfully." });
                }
                return StatusCode(500, "Deleting User Questionnaire Failed");
            }

        }
    }
}
