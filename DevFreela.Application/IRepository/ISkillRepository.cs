using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
        Task<Skill> CreateAsync(Skill skill);

    }
}
