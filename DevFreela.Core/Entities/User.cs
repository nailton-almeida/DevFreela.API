namespace DevFreela.Core.Entities;

public class User  
{
    public User(string fullname, string email, DateTime birthday)
    {
       
        Fullname = fullname;
        Email = email;
        Birthday = birthday;
        CreatedAt = DateTime.Now;
        IsActive = true;

        Skills = new List<UserSkill>();
        OwnerProject = new List<Project>();
        FreelancerProjet = new List<Project>();
    }
    public int Id { get; private set; }
    public string Fullname { get; private set ; }
    public string Email { get;  private set ; }
    public DateTime Birthday { get;  private set  ; }
    public DateTime CreatedAt { get;  private set ; }
    public bool IsActive { get; private set; }
    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnerProject { get; private set; }
    public List<Project> FreelancerProjet { get; private set; }
    public List<ProjectComment> Comments { get; private set; }


    public void UpdateUser(string fullname, string email, DateTime birthday)
    {
        Fullname = fullname;
        Email = email;
        Birthday = birthday;
    }

    public void InactiveUser(bool isActive)
    { IsActive = isActive; }
}
