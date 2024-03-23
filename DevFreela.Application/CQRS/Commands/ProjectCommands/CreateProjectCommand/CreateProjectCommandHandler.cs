using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid?>
{
    private readonly IProjectRepository _repository;

    public CreateProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<Guid?> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var projectInput = new Project(
                command.Title,
                command.Description,
                command.ClientID,
                command.FreelancerID,
                command.TotalCost,
                command.StartedAt,
                command.FinishedAt);

        return await _repository.CreateProjectAsync(projectInput);




    }
}
