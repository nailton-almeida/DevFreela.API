using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;

public class ProjectChangeStatusCommand : IRequest<bool>
{
    public Guid IdProject { get; set; }
    public int Status { get; set; }
}

