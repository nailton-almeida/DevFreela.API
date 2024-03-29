using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
        Task<Skill> CreateAsync(Skill skill);

    }
}
