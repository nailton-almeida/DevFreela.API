using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.InputModel;
using DevFreela.Application.InputModels;
using DevFreela.Application.Interfaces;


namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;
    
    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
       
    }

    [HttpGet]
    public async Task<ActionResult<ProjectViewModel>> GetAll()
    {
        var projectsLists = await _projectRepository.GetAllAsync();
        return Ok(projectsLists); 
        
    }
        
    [HttpGet("projectById/{id}")]
    public async Task<ActionResult<ProjectViewModel>> ProjectById([FromRoute] Guid id)
    {
         
        var project =  await _projectRepository.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
      
        return Ok(project);
    }


    [HttpGet("projectByUserId/{id}")]
    public async Task<ActionResult<ProjectViewModel>> ProjectbyUserId([FromRoute] int id)   
    {
       var projectsByUser = await _projectRepository.GetByUserIdAsync(id);
      
        if (projectsByUser == null)
            return NotFound();

        return Ok(projectsByUser);

    }


    [HttpPost]
    public async Task<ActionResult> Post([FromBody] NewProjectInputModel project)
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
    public async Task<ActionResult> PostComent([FromRoute] Guid id, [FromBody] CreateCommentInputModelcs comment)
    {
       var commentCreated =  await _projectRepository.PostComentsAsync(id, comment);
        if (commentCreated)
            return NoContent();
            
        return BadRequest();
    }

    [HttpPut("ChangeStatus/{id}/{status}")]
    public async Task<ActionResult> ProjectChangeStatus([FromRoute] Guid id,  int status)
    {
        var changeStatus = await _projectRepository.ProjectChangeStatusAsync(id, status);
        if (changeStatus) 
        {
            return NoContent();
        }
        return BadRequest();

    }
     
}

 