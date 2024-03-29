using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;


namespace DevFreela.Application.Validation.Queries;

public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
        RuleFor(project => project.Id)
            .NotEmpty()
                .WithMessage("Project id is required")
            .Must(IdValidatorHelper.IdIsGuid)
               .WithMessage("Project id must be an Guid");
    }
}
