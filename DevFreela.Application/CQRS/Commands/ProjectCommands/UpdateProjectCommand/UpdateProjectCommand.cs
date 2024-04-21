using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;

public class UpdateProjectCommand : IRequest<Guid?>
{

    //public UpdateProjectCommand(Guid id, string title, string description, decimal totalCost, DateTime startedAt, DateTime finishedAt, List<Skill> skills)
    //{
    //    Id = id;
    //    Title = title;
    //    Description = description;
    //    TotalCost = totalCost;
    //    StartedAt = startedAt;
    //    FinishedAt = finishedAt;
    //    Skills = skills;
    //}

    //public UpdateProjectCommand(Guid id, string title, string description, decimal totalCost, DateTime startAt, DateTime finishedAt)
    //{
    //    Id = id;
    //    Title = title;
    //    Description = description;
    //    TotalCost = totalCost;
    //    StartedAt = startAt;
    //    FinishedAt = finishedAt;
    //}

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TotalCost { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public List<Skill?> Skills { get; set; }
}
