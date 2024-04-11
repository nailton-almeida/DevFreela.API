using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.LoginUserCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _serviceAuth;

        public LoginUserCommandHandler(IUserRepository userRepository, IAuthService serviceAuth)
        {
            _userRepository = userRepository;
            _serviceAuth = serviceAuth;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = await _serviceAuth.GeneratePasswordHash256(request.Password);
            var loginUser = await _userRepository.LoginUserAsync(request.Email, passwordHash);

            if (loginUser is not null)
            {
                var token = await _serviceAuth.GenerateTokenJWT(request.Email, (int)(loginUser.Role));
                return new LoginUserViewModel(

                        request.Email,
                        "Bearer " + token,
                        DateTime.Now.AddHours(8)

                    );
            }

            return null;
        }
    }
}
