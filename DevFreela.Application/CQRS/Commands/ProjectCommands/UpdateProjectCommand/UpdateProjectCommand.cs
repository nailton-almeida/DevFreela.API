using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;

public class UpdateProjectCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public List<Skill> Skills { get; set; }
}
