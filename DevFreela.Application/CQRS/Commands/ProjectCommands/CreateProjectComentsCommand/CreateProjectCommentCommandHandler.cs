using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;

public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, Guid?>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public CreateProjectCommentCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }
    public async Task<Guid?> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
    {
        var projectExist = await _projectRepository.ProjectExistAsync(command.IdProject);
        var userHasProject = await _userRepository.UsersHasProjectAsync(command.IdUser);

        if (userHasProject && projectExist is not null)
        {
            var newComment = new ProjectComment(command.Comment, command.IdProject, command.IdUser);
            return await _projectRepository.PostComentsAsync(newComment);
        }
        return null;
    }
}
