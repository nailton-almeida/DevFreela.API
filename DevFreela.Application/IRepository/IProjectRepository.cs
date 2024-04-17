using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<List<Project>?> GetByUserIdAsync(int id);
        Task<Guid?> CreateProjectAsync(Project project);
        Task<Guid?> UpdateProjectAsync(UpdateProjectCommand updateProject);
        Task<bool> ProjectChangeStatusAsync(Guid id, int status);
        Task<Guid?> PostComentsAsync(ProjectComment comment);
        Task<bool> ProjectExistAsync(Guid idProject, int idUser);

    }
}
