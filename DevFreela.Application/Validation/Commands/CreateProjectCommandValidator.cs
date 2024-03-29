using DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;
using FluentValidation;

namespace DevFreela.Application.Validation.Commands
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(projectTitle => projectTitle.Title)
                .NotEmpty()
                    .WithMessage("Project need a title")
                .MinimumLength(8)
                    .WithMessage("Project need a title with more than 8 characters lenght")
                .MaximumLength(50)
                    .WithMessage("Project neeed a title with less than 50 characters lenght");


            RuleFor(projectDescription => projectDescription.Description)
                .NotEmpty()
                    .WithMessage("Project need a description")
                .MinimumLength(8)
                    .WithMessage("Project need a title with more than 8 characters lenght")
                .MaximumLength(1000)
                    .WithMessage("Project neeed a title with less than 1000 characters lenght");

            RuleFor(projectCost => projectCost.TotalCost)
                .NotEmpty()
                    .WithMessage("Project need a total cost")
                .GreaterThan(0)
                    .WithMessage("Project need a total cost greater than 0");

            RuleFor(projectStartDate => projectStartDate.StartedAt)
                .NotEmpty()
                    .WithMessage("Project need a start date")
                .GreaterThan(DateTime.Now)
                    .WithMessage("Project need a start date greater than today");

            RuleFor(projectEndDate => projectEndDate.FinishedAt)
                .NotEmpty()
                    .WithMessage("Project need a finish date")
                .GreaterThan(projectEndDate => projectEndDate.StartedAt)
                    .WithMessage("Project need a finish date greater than start date");



        }
    }
}
