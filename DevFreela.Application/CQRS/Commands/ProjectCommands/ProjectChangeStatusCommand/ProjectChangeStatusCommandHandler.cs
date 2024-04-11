using DevFreela.Application.Interfaces;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;

public class ProjectChangeStatusCommandHandler : IRequestHandler<ProjectChangeStatusCommand, bool>
{
    private readonly IProjectRepository _projectRepository;

    public ProjectChangeStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(ProjectChangeStatusCommand request, CancellationToken cancellationToken)
    {
        return await _projectRepository.ProjectChangeStatusAsync(request.IdProject, request.Status);

    }
}

