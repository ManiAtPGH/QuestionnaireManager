using Microsoft.AspNetCore.Mvc;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireQuestionController : ControllerBase
    {
        private readonly IQuestionnaireQuestionService _questionnaireQuestionService;

        public QuestionnaireQuestionController(IQuestionnaireQuestionService questionnaireQuestionService)
        {
            _questionnaireQuestionService = questionnaireQuestionService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _questionnaireQuestionService.GetQuestionnaireQuestions();
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
            var data = _questionnaireQuestionService.GetQuestionnaireQuestion(id);
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
        public IActionResult Post(AddQuestionnaireQuestionModel model)
        {
            if (model != null)
            {
                var roleClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                //if (roleClaim != null)
                //{
                //    model.CreatedById = new Guid(roleClaim.Value);
                //}

                if (_questionnaireQuestionService.CheckForDuplication(model.QuestionnaireId, model.QuestionnaireQuestion))
                {
                    return BadRequest("Duplicate question names are not allowed, please check");
                }

                var result = _questionnaireQuestionService.AddQuestionnaireQuestion(model);

                if (result > 0)
                {
                    return StatusCode(201);
                }

                return StatusCode(500);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AddQuestionnaireQuestionModel value)
        {
            if (value == null || id == null)
            {
                return BadRequest();
            }
            else
            {
            
                return Ok(value);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { message = "QuestionnaireQuestion deleted successfully." });
            }
            //var userToDelete = _userRepository.GetUserById(id);
            //if (userToDelete == null)
            //{
            //    return NotFound();
            //}

            //_userRepository.DeleteUser(userToDelete);
            //_userRepository.SaveChanges();

            //return NoContent();
        }
    }
}
