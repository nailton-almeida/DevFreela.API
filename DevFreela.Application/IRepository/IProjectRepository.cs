using DevFreela.Application.ViewModels;
using DevFreela.Application.InputModel;
using DevFreela.Application.InputModels;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<List<Project>?> GetByUserIdAsync(int id);
        Task<Guid?> CreateProjectAsync(NewProjectInputModel project);
        Task<bool> UpdateProjectAsync(Guid id, UpdateProjectInputModel updateProject);
        Task<bool> ProjectChangeStatusAsync(Guid id, int status);
        Task<bool> PostComentsAsync(Guid id, CreateCommentInputModelcs commentary);

    }
}
