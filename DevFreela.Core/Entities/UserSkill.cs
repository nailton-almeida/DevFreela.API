namespace DevFreela.Core.Entities;

public class UserSkill
{
    public UserSkill()
    {

    }

    public UserSkill(int userId, int skillId)
    {
        IdUser = userId;
        IdSkill = skillId;
    }

    public int Id { get; private set; }
    public int IdUser { get; private set; }
    public User User { get; private set; }
    public int IdSkill { get; private set; }
    public Skill Skill { get; private set; }
}