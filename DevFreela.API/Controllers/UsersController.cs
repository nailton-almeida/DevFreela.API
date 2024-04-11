using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Application.CQRS.Commands.UserCommands.LoginUserCommand;
using DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery;
using DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
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

    [HttpPost("/createuser")]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand user)
    {
        var newUser = await _mediator.Send(user);

        if (newUser is not null)
        {
            return Created();
        }

        return BadRequest();
    }

    [HttpPut("/edituser")]
    [Authorize(Roles = "Freelances, Client")]
    public async Task<IActionResult> Put([FromBody] EditUserCommand command)
    {
        var updateUser = await _mediator.Send(command);

        if (updateUser)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPut("inactive/{id}")]
    [Authorize(Roles = "Freelances, Client")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        var command = new GetUserByIdQuery(id);
        var userInactive = await _mediator.Send(command);
        if (userInactive is not null)
            return NoContent();
        return NotFound();
    }

    [HttpPut("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var loginUser = await _mediator.Send(command);

        if (loginUser is not null)
        {
            return Ok(loginUser);
        }

        return BadRequest("Check your credencials");

    }
}
