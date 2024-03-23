using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;

public class CreateProjectCommand : IRequest<Guid?>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ClientID { get; set; }
    public int FreelancerID { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public List<Skill> Skills { get; set; }
}
