using DevFreela.Application.CQRS.Commands.ProjectCommands.FinishProjectCommand;
using DevFreela.Application.Validation.Helpers;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands
{
    public class FinishProjectCommandValidator : AbstractValidator<FinishProjectCommand>
    {
        public FinishProjectCommandValidator()
        {
            RuleFor(id => id.IdProject)
                .NotEmpty()
                    .WithMessage("Request needs project id")
                .Must(IdValidatorHelper.IdIsGuid);


            RuleFor(s => s.CardName)
                .NotEmpty()
                    .WithMessage("Request needs project card name");


            RuleFor(d => d.CardNumber)
               .NotEmpty()
                   .WithMessage("Request needs project card number")
                .CreditCard();

            RuleFor(e => e.CVV)
               .NotEmpty()
                    .WithMessage("Request needs project card cvv number");

            RuleFor(f => f.ValidateDate)
                .NotEmpty();
           }
    }
}
