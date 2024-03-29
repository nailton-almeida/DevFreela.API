using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery;
using DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UsersViewModel>> Get()
    {
        return await _mediator.Send(new GetAllUsersQuery());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        
        var user = await _mediator.Send(query);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand user)
    {
        var newUser = await _mediator.Send(user);

        if (newUser is not null)
        {
            return CreatedAtAction(nameof(GetById), new { id = newUser }, user);
        }

        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> Put(EditUserCommand command)
    {
        var updateUser = await _mediator.Send(command);

        if (updateUser)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPut("inactive/{id}")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        var command = new GetUserByIdQuery(id);
        var userInactive = await _mediator.Send(command);
        if (userInactive is not null)
            return NoContent();
        return NotFound();
    }
}
