using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.FinishProjectCommand
{
    public class FinishProjectCommand : IRequest<bool>
    {
        public Guid IdProject { get; set; }
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? CVV { get; set; }
        public string? ValidateDate { get; set; }

    }
}
