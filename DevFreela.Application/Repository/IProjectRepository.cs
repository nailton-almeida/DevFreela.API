using DevFreela.Application.ViewModels;
using DevFreela.Application.InputModel;
using DevFreela.Application.InputModels;

namespace DevFreela.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectViewModel>> GetAllAsync();
        Task<ProjectDetailsViewModel> GetByIdAsync(Guid id);
        Task<IEnumerable<ProjectViewModel>> GetByUserIdAsync(int id);
        //Task<IEnumerable<ProjectViewModel>> GetBySkill(List<string> skills);
        Task<Guid?> CreateProjectAsync(NewProjectInputModel project);
        Task<bool> UpdateProjectAsync(Guid id, UpdateProjectInputModel updateProject);
        Task<bool> ProjectChangeStatusAsync(Guid id, int status);
        Task<bool> PostComentsAsync(Guid id, CreateCommentInputModelcs commentary);

    }
}
