using DevFreela.Application.InputModel;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }


    [HttpGet]
    public async Task<IEnumerable<UsersViewModel>> Get()
    {
        return await _userRepository.GetAllAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var userExist = await _userRepository.GetByIdAsync(id);
        if (userExist == null)
           return NotFound();
        
        return Ok(userExist);
    }

    [HttpPost]
    public async Task<ActionResult<UsersViewModel>> Post([FromBody] UsersInputModel user)
    {
        var createUser = await _userRepository.CreateUserAsync(user);
        if (createUser is not null)
        {

            return CreatedAtAction(nameof(GetById), new { id = createUser }, user);
        }
        return BadRequest();
               

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsersViewModel>> Put(int id, [FromBody] UsersInputModel inputUser)
    {

        var updateUser = await _userRepository.EditUserAsync(id, inputUser);

        if (updateUser)
        {

            return NoContent();

        }
        return NotFound();


    }

    [HttpPut("inactive/{id}")]
    public async Task<ActionResult> InactiveUser(int id)
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
