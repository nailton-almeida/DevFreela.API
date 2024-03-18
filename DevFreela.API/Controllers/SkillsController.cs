using DevFreela.Application.InputModels;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SkillsController : ControllerBase
    {
       private readonly ISkillRepository _skillRepository;
        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }



        [HttpGet]
        public async Task<ActionResult<SkillsViewModel>> Get() 
        {
            var listSkill = await _skillRepository.GetAllAsync();
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
        public async Task<ActionResult> Post([FromBody] SkillsInputModel skill)
        {
            var skillCreate = await _skillRepository.CreateAsync(skill);
            if (skillCreate == null)
            {
                return BadRequest();

            }
            return NoContent();
           //return CreatedAtAction(nameof(GetSkillsById), new { id = skillCreate }, skill);
        }







    }
}
