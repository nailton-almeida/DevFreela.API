using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid?>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }
    public async Task<Guid?> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var projectInput = new Project(
                command.Title,
                command.Description,
                command.ClientID,
                command.TotalCost,
                command.StartedAt,
                command.FinishedAt);

        var userExist = await _userRepository.UsersExistAndActivateAsync(command.ClientID);
        if (userExist is not null)
            return await _projectRepository.CreateProjectAsync(projectInput);

        return null;
    }
}
