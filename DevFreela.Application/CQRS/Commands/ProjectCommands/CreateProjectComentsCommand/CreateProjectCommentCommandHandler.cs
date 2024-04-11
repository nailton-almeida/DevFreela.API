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
    public async Task<Guid?> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
    {
        var newComment = new ProjectComment(request.Comment, request.IdProject, request.IdUser);

        var createComment = await _projectRepository.PostComentsAsync(newComment);

        if (createComment is not null) return createComment;

        return null;
    }
}
