using DevFreela.Application.Interfaces;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;


public class EditUserCommandHandler : IRequestHandler<EditUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    public EditUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.EditUserAsync(request);
    }
}
