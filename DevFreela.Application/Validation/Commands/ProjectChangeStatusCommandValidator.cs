using DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;


namespace DevFreela.Application.Validation.Commands;

public class ProjectChangeStatusCommandValidator : AbstractValidator<ProjectChangeStatusCommand>
{
    public ProjectChangeStatusCommandValidator()
    {
        RuleFor(projectId => projectId.IdProject)
            .NotEmpty()
                .WithMessage("Project id is required")
        .Must(IdValidatorHelper.IdIsGuid)
               .WithMessage("Project id must be an Guid");

        RuleFor(projectStatus => projectStatus.Status)
            .NotEmpty()
                .WithMessage("Status is required")
            .IsInEnum()
                .WithMessage("Status is invalid");
    }
}
