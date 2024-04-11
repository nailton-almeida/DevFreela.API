using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.Validation.Helpers;
using DevFreela.Core.Enums;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
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

        RuleFor(userPassword => userPassword.Password)
            .NotEmpty()
                .WithMessage("Password field is required")
            .Must(PasswordPatternValidatorHelper.CheckPatternPasswordPattern)
                .WithMessage("Password must have at least:" +
                " Start with a letter," +
                " Between 8 and 32 characters," +
                " One numeral," +
                " One uppercase letter," +
                " One lowercase letter, " +
                " One special character");

        RuleFor(p => p.Role)
            .NotEmpty()
                .WithMessage("User needs a role");

        RuleFor(userBirthday => userBirthday.Birthday)
            .NotEmpty()
                .WithMessage("Birthday is required")
            .LessThan(DateTime.Now)
                .WithMessage("Birthday must be less than today's date");

    }


    public bool CheckRoleExist(UserRoleEnum value)
    {
        return Enum.IsDefined(typeof(UserRoleEnum), value);
    }
}
