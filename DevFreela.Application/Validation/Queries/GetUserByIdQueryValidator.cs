using DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;


namespace DevFreela.Application.Validation.Queries
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(user => user.Id)
                .NotEmpty()
                    .WithMessage("User id is required")
                .Must(IdValidatorHelper.IdIsInt)
                  .WithMessage("User id must be an integer");
        }


    }


}
