using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.InputModel;
using DevFreela.Application.InputModels;
using MediatR;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;
using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;


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
    public async Task<IActionResult> Post([FromBody] NewProjectInputModel project)
    {
        var createProject = await _projectRepository.CreateProjectAsync(project);
        if (createProject == null)
            BadRequest();
        return CreatedAtAction(nameof(ProjectById), new { id = createProject }, project);
    }

    [HttpPut("updateDetails/{id}")]
    public async Task<ActionResult> UpdateProjectDetails([FromRoute] Guid id, [FromBody] UpdateProjectInputModel project)
    {
        var projectUpdate = await _projectRepository.UpdateProjectAsync(id, project);
        if (projectUpdate is false)
        {
            return BadRequest();
        }
        return NoContent();
    }



    [HttpPost("comments/{id}")]
    public async Task<IActionResult> PostComent([FromRoute] Guid id, [FromBody] CreateCommentInputModelcs comment)
    {
        var commentCreated = await _projectRepository.PostComentsAsync(id, comment);
        if (commentCreated)
            return NoContent();

        return BadRequest();
    }

    [HttpPut("ChangeStatus/{id}/{status}")]
    public async Task<ActionResult> ProjectChangeStatus([FromRoute] Guid id, int status)
    {
        var changeStatus = await _projectRepository.ProjectChangeStatusAsync(id, status);
        if (changeStatus)
        {
            return NoContent();
        }
        return BadRequest();

    }

}

