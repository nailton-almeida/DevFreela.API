using DevFreela.Application.CQRS.Commands.SkillCommand;
using DevFreela.Application.CQRS.Queries.SkillQueries;
using DevFreela.Application.InputModels;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillsController : ControllerBase
    {
       private readonly IMediator _iMediator;
        public SkillsController(IMediator iMediator)
        {
            _iMediator = iMediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var listSkill = await _iMediator.Send(new GetAllSkillsQuery());
            return Ok(listSkill);
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<SkillsViewModel>> GetSkillsById(int id)
        //{
        //    var skill = await _skillRepository.GetByIdAsync(id);
        //    if (skill != null)
        //        return Ok(skill);

        //    return NotFound("Not found skill");
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserSkills>> GetUserBySkills(int id)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
        {
            
            var skillCreate = await _iMediator.Send(command);
            
            if (skillCreate == null)
            {
                return BadRequest();

            }
            return NoContent();
           //return CreatedAtAction(nameof(GetSkillsById), new { id = skillCreate }, skill);
        }







    }
}
