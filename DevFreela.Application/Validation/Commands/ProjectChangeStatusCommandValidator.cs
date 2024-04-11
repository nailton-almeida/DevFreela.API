using DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;
using DevFreela.Application.Validation.Helpers;
using DevFreela.Core.Enums;
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
            .Must(ValueIsEnum)
                .WithMessage("Status is invalid");
    }

    public bool ValueIsEnum(int status)
    {
        return Enum.IsDefined(typeof(ProjectStatusEnum), status);
    }
}
