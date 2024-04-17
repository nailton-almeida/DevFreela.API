using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;

public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, Guid?>
{
    private readonly IProjectRepository _projectRepository;
    public CreateProjectCommentCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    public async Task<Guid?> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
    {
       var projectExist = await _projectRepository.ProjectExistAsync(command.IdProject, command.IdUser);
 
        if (projectExist) 
        {
            var newComment = new ProjectComment(command.Comment, command.IdProject, command.IdUser);
            return await _projectRepository.PostComentsAsync(newComment);
        }  

        return null;
    }
}
