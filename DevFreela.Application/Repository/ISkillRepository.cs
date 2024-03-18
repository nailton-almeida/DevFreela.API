using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<SkillsViewModel>> GetAllAsync();
       // Task<SkillsViewModel> GetByIdAsync(int id);
        Task<int?> CreateAsync(SkillsInputModel skill);

    }
}
