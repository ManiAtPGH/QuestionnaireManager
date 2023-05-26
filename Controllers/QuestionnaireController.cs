using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestionnaireManagerPOC.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _questionnaireService.GetQuestionnaire();
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
            var data = _questionnaireService.GetQuestionnaire(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                if(data.QuestionnaireName == null)
                    return NotFound();
                return Ok(data);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BaseQuestionnaireModel value)
        {
            
            if (value == null)
            {
                return BadRequest();
            }
            else
            {
                int response = _questionnaireService.AddQuestionnaire(value);

                if(response == 0)
                {
                    return StatusCode(500, "Unable to add resource, Please check if the given Name already exists");
                }
                return Ok(response);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] BaseQuestionnaireModel model)
        {
            if (model == null || id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                var questionnaireToUpdate = _questionnaireService.GetQuestionnaire(id);
                if (questionnaireToUpdate.QuestionnaireName == null)
                {
                    return NotFound();
                }
                if(_questionnaireService.UpdateQuestionnaire(model, id) <= 0)
                {
                    return StatusCode(501, "Unable to update the Questionnaire");
                }
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            else
            {
                var questionnaireToDelete = _questionnaireService.GetQuestionnaire(id);
                if (questionnaireToDelete.QuestionnaireName == null)
                {
                    return NotFound();
                }
                _questionnaireService.DeleteQuestionnaire(id);
                return Ok(new { message = "Questionnaire deleted successfully." });
            }
        }

    }
}
