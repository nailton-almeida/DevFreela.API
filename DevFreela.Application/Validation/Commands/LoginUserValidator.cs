using DevFreela.Application.CQRS.Commands.UserCommands.LoginUserCommand;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
                .WithMessage("Email field is required")
            .EmailAddress()
                .WithMessage("Must be a email valid");

        RuleFor(p => p.Password)
            .NotEmpty()
                .WithMessage("Password field is required");
    }
}
