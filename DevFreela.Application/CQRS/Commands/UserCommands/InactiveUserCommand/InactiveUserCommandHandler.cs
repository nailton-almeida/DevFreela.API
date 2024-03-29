using DevFreela.Application.Interfaces;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.InactiveUserCommand;

public class InactiveUserCommandHandler : IRequestHandler<InactiveUserCommand, bool>
{
   private readonly IUserRepository _userRepository;
    
    public InactiveUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(InactiveUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.InactiveUserAsync(request.Id);
    }
}
