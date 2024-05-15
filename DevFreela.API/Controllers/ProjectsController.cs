using DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.FinishProjectCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Freelancer")]
    public async Task<IActionResult> GetAll()
    {
        var getAllProjects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(getAllProjects);
    }

    [HttpGet("projectById/{id}")]
    [Authorize(Roles = "Freelancer, Client")]
    public async Task<IActionResult> ProjectById(Guid id)
    {
        var query = new GetProjectByIdQuery(id);
        var project = await _mediator.Send(query);
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpGet("projectByUserId/{id}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> ProjectByUserId(int id)
    {
        var query = new GetProjectByUserIdQuery(id);
        var projectsByUser = await _mediator.Send(query);

        if (projectsByUser == null)
        {
            return NotFound();
        }

        return Ok(projectsByUser);
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
    {
        var createProject = await _mediator.Send(command);
        if (createProject == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(ProjectById), new { id = createProject }, command);
    }

    [HttpPut("updateDetails/{id}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateProjectDetails(Guid id, [FromBody] UpdateProjectCommand command)
    {  
        var projectUpdate = await _mediator.Send(command);
        if (projectUpdate == null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPost("comments")]
    [Authorize(Roles = "Freelancer, Client")]
    public async Task<IActionResult> PostComment([FromBody] CreateProjectCommentCommand command)
    {
        var commentCreated = await _mediator.Send(command);
        if (commentCreated == null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPut("changeStatus/{id}/{status}")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> ProjectChangeStatus(Guid id, int status)
    {

        var command = new ProjectChangeStatusCommand(id, status);
        var changeStatus = await _mediator.Send(command);
        if (changeStatus)
        {
            return NoContent();
        }
        return BadRequest();
    }

    [HttpPost("finishProject")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> FinishProject([FromBody] FinishProjectCommand command)
    {
        await _mediator.Send(command);

        return Accepted("Project will be finish when payment was processed.");
    }
}

