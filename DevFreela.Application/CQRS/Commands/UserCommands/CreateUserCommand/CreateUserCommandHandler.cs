using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int?>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _serviceAuth;

    public CreateUserCommandHandler(IUserRepository userRepository, IAuthService serviceAuth)
    {
        _userRepository = userRepository;
        _serviceAuth = serviceAuth;
    }

    public async Task<int?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = await _serviceAuth.GeneratePasswordHash256(request.Password);

        var user = new User(request.Fullname, request.Email, passwordHash, request.Birthday, request.Role);

        var userCreated = await _userRepository.CreateUserAsync(user);

        if (userCreated is not null)
        {
            return userCreated;
        }

        return null;
    }
}
