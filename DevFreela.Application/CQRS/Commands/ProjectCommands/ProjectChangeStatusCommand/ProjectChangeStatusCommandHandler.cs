using DevFreela.Application.Interfaces;
using DevFreela.Core.Enums;
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
        var projectExist = await _projectRepository.ProjectExistAsync(request.IdProject);

        if (projectExist is not null)
        {
            if ((int)projectExist.Status != (int)ProjectStatusEnum.Finish && request.Status < (int)projectExist.Status)
            {
                return await _projectRepository.ProjectChangeStatusAsync(request.IdProject, request.Status);
            }
        }

        return false;
    }
}

