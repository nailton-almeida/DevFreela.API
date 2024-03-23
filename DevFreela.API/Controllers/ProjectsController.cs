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
    public async Task<IActionResult> ProjectById(GetProjectByIdQuery query)
    {
        var project = _mediator.Send(query);
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }


    [HttpGet("projectByUserId/{id}")]
    public async Task<IActionResult> ProjectbyUserId(GetProjectByUserIdQuery query)
    {
        var projectsByUser = await _mediator.Send(query);

        if (projectsByUser == null)
            return NotFound();

        return Ok(projectsByUser);

    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProjectCommand command)
    {
        var createProject = await _mediator.Send(command);
        if (createProject == null)
            BadRequest();
        return CreatedAtAction(nameof(ProjectById), new { id = createProject }, command);
    }

    [HttpPut("updateDetails/{id}")]
    public async Task<ActionResult> UpdateProjectDetails(UpdateProjectCommand command)
    {
        var projectUpdate = await _mediator.Send(command);
        if (projectUpdate is null)
        {
            return BadRequest();
        }
        return NoContent();
    }



    [HttpPost("comments/{id}")]
    public async Task<IActionResult> PostComent(CreateProjectCommentCommand command)
    {
        var commentCreated = await _mediator.Send(command);
        if (commentCreated is not null)
            return NoContent();

        return BadRequest();
    }

    [HttpPut("changeStatus/{id}/{status}")]
    public async Task<ActionResult> ProjectChangeStatus(ProjectChangeStatusCommand command)
    {
        var changeStatus = await _mediator.Send(command);
        if (changeStatus)
        {
            return NoContent();
        }
        return BadRequest();

    }

}

