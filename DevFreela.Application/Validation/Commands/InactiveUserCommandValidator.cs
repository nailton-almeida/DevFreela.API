
using DevFreela.Application.CQRS.Commands.UserCommands.InactiveUserCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands;

public class InactiveUserCommandValidator : AbstractValidator<InactiveUserCommand>
{

    public InactiveUserCommandValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty()
                .WithMessage("User id is required")
            .Must(IdValidatorHelper.IdIsInt)
               .WithMessage("Project id must be an int");
    }
}
