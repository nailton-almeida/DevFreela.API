namespace DevFreela.Core.Entities;

public class Skill
{
    public Skill()
    {
        
    }
    public Skill(string name, string typeSkill)
    {
        Name = name;
        TypeSkill = typeSkill;
    }

    internal Skill(int id, string name, string typeSkill)
    {
        Id = id;
        Name = name;
        TypeSkill = typeSkill;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string TypeSkill { get; private set; }

}
