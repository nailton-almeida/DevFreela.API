using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int?>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Fullname, request.Email, request.Birthday);
        
        var userCreated = await _userRepository.CreateUserAsync(user);

        if (userCreated == null)
        {
            return userCreated;
        }

        return null;
    }
}
