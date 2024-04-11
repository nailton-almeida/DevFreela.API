
namespace DevFreela.Core.Entities
{
    public class ProjectSkill


    {
        //public ProjectSkill()
        //{

        //}

        public ProjectSkill(Guid idProject, int idSkill)
        {
            IdProject = idProject;
            IdSkill = idSkill;
        }

        public int Id { get; private set; }
        public Guid IdProject { get; private set; }
        public Project Project { get; private set; }
        public int IdSkill { get; private set; }
        public Skill Skill { get; private set; }
    }
}
