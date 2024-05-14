using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(project => project.Id)
            .NotEmpty()
                .WithMessage("Project id is required")
                .Must(IdValidatorHelper.IdIsGuid)
                   .WithMessage("Project id must be an Guid");

        RuleFor(project => project.Title)
            .NotEmpty()
                .WithMessage("Title is required")
             .MinimumLength(8)
                .WithMessage("Project need a title with more than 8 characters lenght")
            .MaximumLength(50)
                .WithMessage("Title is too long");

        RuleFor(project => project.Description)
            .NotEmpty()
                .WithMessage("Description is required")
            .MinimumLength(50)
                .WithMessage("Project need a title with more than 50 characters lenght")
            .MaximumLength(1000)
                .WithMessage("Project need a title with less than 1000 characters lenght");

        RuleFor(project => project.TotalCost)
            .GreaterThan(0)
                .WithMessage("Total cost must be greater than 0");

        RuleFor(project => project.StartedAt)
            .NotEmpty()
                .WithMessage("StartedAt is required")
            .GreaterThan(DateTime.Now)
                .WithMessage("StartedAt must be greater than today");

        RuleFor(project => project.FinishedAt)
            .NotEmpty()
                .WithMessage("FinishedAt is required")
            .GreaterThanOrEqualTo(project => project.StartedAt)
                .WithMessage("FinishedAt must be greater than StartedAt");
    }
}
