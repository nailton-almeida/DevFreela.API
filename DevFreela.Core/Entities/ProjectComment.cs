namespace DevFreela.Core.Entities;

public class ProjectComment
{
    public ProjectComment()
    {

    }
    public ProjectComment(string comment, Guid projectId, int userId)
    {
        Comment = comment;
        IdProject = projectId;
        IdUser = userId;
        CreateAt = DateTime.Now;
    }
    public Guid Id { get; private set; }
    public string Comment { get; private set; }
    public Guid IdProject { get; private set; }
    public Project Project { get; private set; }
    public int IdUser { get; private set; }
    public User User { get; private set; }
    public DateTime CreateAt { get; private set; }

}