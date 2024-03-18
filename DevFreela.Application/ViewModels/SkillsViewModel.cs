
namespace DevFreela.Application.ViewModels
{
    public class SkillsViewModel
    {
        public SkillsViewModel(int id, string name, string typeSkills)
        {
            Id = id;
            Name = name;
            TypeSkills = typeSkills;
    
        }
        public int Id  { get; private set; }
        public string Name { get; private set; }
        public string TypeSkills { get; private set; }
    }
}
