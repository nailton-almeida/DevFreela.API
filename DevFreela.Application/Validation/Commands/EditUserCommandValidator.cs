using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;


namespace DevFreela.Application.Validation.Commands;

public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
{
    public EditUserCommandValidator()
    {
        RuleFor(user => user.Id)
            .NotEmpty()
                .WithMessage("User id is required")
            .Must(IdValidatorHelper.IdIsInt)
               .WithMessage("Project id must be an int");


        RuleFor(userFullname => userFullname.Fullname)
            .NotEmpty()
                .WithMessage("Fullname is required")
            .MinimumLength(8)
                .WithMessage("Fullname need a text with more than 8 characters lenght")
            .MaximumLength(30)
                .WithMessage("Fullname need a text with less than 30 characters lenght");

        RuleFor(userEmail => userEmail.Email)
            .NotEmpty()
                .WithMessage("Email is required")
            .EmailAddress()
                .WithMessage("Email is invalid");

        RuleFor(userBirthday => userBirthday.Birthday)
            .NotEmpty()
                .WithMessage("Birthday is required")
            .LessThan(DateTime.Now)
                .WithMessage("Birthday is invalid");
    }
}
