using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;

namespace DevFreela.Application.Validation.Queries;

public class GetProjectByUserIdQueryValidator : AbstractValidator<GetProjectByUserIdQuery>
{
    public GetProjectByUserIdQueryValidator()
    {
        RuleFor(project => project.Id)
            .NotEmpty()
                .WithMessage("Project id is required")
            .Must(IdValidatorHelper.IdIsInt);
    }


}
