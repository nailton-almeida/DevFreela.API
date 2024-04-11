using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project
{

    public Project()
    {

    }

    public Project(string title, string description, int clientID, int freelacerId, decimal totalCost, DateTime startedAt, DateTime finishedAt)
    {
        Id = new Guid();
        Title = title;
        Description = description;
        IdClient = clientID;
        IdFreelancer = freelacerId;
        TotalCost = totalCost;
        StartedAt = startedAt;
        FinishedAt = finishedAt;

        CreatedAt = DateTime.Now;
        Status = ProjectStatusEnum.Created;
        Comments = new List<ProjectComment>();

    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int IdClient { get; private set; }
    public User Client { get; private set; }
    public int IdFreelancer { get; private set; }
    public User Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime FinishedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public void UpdateProject(string title, string description, decimal totalCost, DateTime startedAt, DateTime finishedAt)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
        StartedAt = startedAt;
        FinishedAt = finishedAt;
    }

    public void UpdateStatus(int status)
    {
        Status = (ProjectStatusEnum)status;
    }
}