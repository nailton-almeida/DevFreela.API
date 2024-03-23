using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery;
using DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;
using DevFreela.Application.InputModel;
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
    public async Task<IActionResult> GetById(GetUserByIdQuery query)
    {
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UsersInputModel inputUser)
    {

        var updateUser = await _userRepository.EditUserAsync(id, inputUser);

        if (updateUser)
        {

            return NoContent();

        }
        return NotFound();


    }

    [HttpPut("inactive/{id}")]
    public async Task<IActionResult> InactiveUser(int id)
    {
        var userInactive = await _userRepository.InactiveUserAsync(id);
        if (userInactive)
            return NoContent();
        return NotFound();
    }

    #region to do later
    //[HttpPut("login/{id}")]
    //public async Task<ActionResult> Login(int id, [FromBody] string login)
    //{
    //    // return NoContent();
    //    throw new NotImplementedException();
    //}

    //[HttpPost("resetPassword/{id}")]
    //public Task<ActionResult> ResetPassword(int id , string email, string newpassword)
    //{
    //    throw new NotImplementedException();
    //}
    #endregion

}
