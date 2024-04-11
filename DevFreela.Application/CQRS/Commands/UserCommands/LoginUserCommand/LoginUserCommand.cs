using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.LoginUserCommand;

public class LoginUserCommand : IRequest<LoginUserViewModel>
{
    public string Email { get; set; }
    public string Password { get; set; }

}
