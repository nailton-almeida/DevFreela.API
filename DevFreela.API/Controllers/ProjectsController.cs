using Microsoft.AspNetCore.Mvc;
using MediatR;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;
using DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllProjects = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(getAllProjects);
    }

    [HttpGet("projectById/{id}")]
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
    public async Task<IActionResult> UpdateProjectDetails(Guid id, UpdateProjectCommand command)
    {
        var commandSent = new UpdateProjectCommand(id, command);
        var projectUpdate = await _mediator.Send(commandSent);
        if (projectUpdate == null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPost("comments")]
    public async Task<IActionResult> PostComment(CreateProjectCommentCommand command)
    {
        var commentCreated = await _mediator.Send(command);
        if (commentCreated == null)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpPut("changeStatus/{id}/{status}")]
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
}

