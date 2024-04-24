using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommand : IRequest<int?>
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Birthday { get; set; }
    public int Role { get; set; }
}