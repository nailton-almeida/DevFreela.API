using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;

public class CreateProjectCommentCommand : IRequest<Guid?>
{
    
    public Guid IdProject { get; set; }
    public string Comment { get; set; }
    public int IdUser { get; set; }
}
