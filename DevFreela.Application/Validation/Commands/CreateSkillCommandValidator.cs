using DevFreela.Application.CQRS.Commands.SkillCommand;
using FluentValidation;


namespace DevFreela.Application.Validation.Commands;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(skillName => skillName.Name)
            .NotEmpty()
                .WithMessage("Skill name is required")
            .MinimumLength(2)
                .WithMessage("Skill name need a text with more than 2 characters lenght")
            .MaximumLength(15)
                .WithMessage("Skill name need a text with less than 15 characters lenght");

        RuleFor(skillType => skillType.TypeSkills)
            .NotEmpty()
                .WithMessage("Skill type is required")
            .MinimumLength(5)
                .WithMessage("Skill type need a text with more than 5 characters lenght")
            .MaximumLength(15)
                .WithMessage("Skill type need a text with less than 15 characters lenght");
    }
}
