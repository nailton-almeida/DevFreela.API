using DevFreela.Application.Interfaces;
using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;

public class ProjectChangeStatusCommandHandler : IRequestHandler<ProjectChangeStatusCommand, bool>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IPaymentService _PaymentService;

    public ProjectChangeStatusCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(ProjectChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var projectExist = await _projectRepository.ProjectExistAsync(request.IdProject);

        if (projectExist is not null)
        {
            if (request.Status != (int)ProjectStatusEnum.Finish)
            {
                return await _projectRepository.ProjectChangeStatusAsync(request.IdProject, request.Status);
            }

            else
            {
                var paymentIsDone = await _PaymentService.CheckPaymentAsync();
                var changeStatus = paymentIsDone ? (await _projectRepository.ProjectChangeStatusAsync(request.IdProject, request.Status)) : false;
                return changeStatus;
            }
        }
            
        return false;
    }
}

