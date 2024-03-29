using DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands;

public class CreateProjectCommentCommandValidator : AbstractValidator<CreateProjectCommentCommand>
{
    public CreateProjectCommentCommandValidator()
    {
        RuleFor(projectId => projectId.IdProject)
            .NotEmpty()
                .WithMessage("Comment need a project id")
            .Must(IdValidatorHelper.IdIsGuid)
                   .WithMessage("Project id must be an Guid");

        RuleFor(userId => userId.IdUser)
            .NotEmpty()
                .WithMessage("User need a user id");

        RuleFor(projectComment => projectComment.Comment)
            .NotEmpty()
                .WithMessage("Comment need a text")
            .MinimumLength(20)
                .WithMessage("Comment need a text with more than 5 characters lenght")
            .MaximumLength(500)
                .WithMessage("Comment neeed a text with less than 255 characters lenght");
    }
}
