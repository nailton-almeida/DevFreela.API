using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.InactiveUserCommand;

public class InactiveUserCommand : IRequest<bool>
{
    public int Id { get; set; }

}
