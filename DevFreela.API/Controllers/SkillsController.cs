using DevFreela.Application.CQRS.Commands.SkillCommand;
using DevFreela.Application.CQRS.Queries.SkillQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DevFreela.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSkillCommand command)
        {

            var skillCreate = await _iMediator.Send(command);

            if (skillCreate == null)
            {
                return BadRequest();

            }
            return NoContent();
        }







    }
}
