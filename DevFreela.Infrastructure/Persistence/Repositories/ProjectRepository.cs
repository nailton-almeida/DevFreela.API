using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _dbContext;
       
        public ProjectRepository(DevFreelaDbContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
 
        }

        public async Task<List<Project>> GetAllAsync()
        {
            var projects = await _dbContext.Projects.AsNoTracking().ToListAsync();

            return projects;
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            var project = await _dbContext.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .SingleOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<List<Project>?> GetByUserIdAsync(int id)
        {
           return await _dbContext.Projects.Where(p => p.IdClient == id).ToListAsync();
        }

        public async Task<Guid?> CreateProjectAsync(Project project)
        {
          await _dbContext.Projects.AddAsync(project);
          await _dbContext.SaveChangesAsync();
          return project.Id;
        }

        public async Task<Guid?> UpdateProjectAsync(UpdateProjectCommand project)
        {
            var projectToUpdate = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == project.Id && ((int)p.Status != 4 || (int)p.Status != 5));

            if (projectToUpdate is not null)
            {
                projectToUpdate.UpdateProject(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt);
                await _dbContext.SaveChangesAsync();
                return projectToUpdate.Id;
            }

            return null;
        }


        public async Task<Guid?> PostComentsAsync(ProjectComment comment)
        {
                await _dbContext.ProjectComments.AddAsync(comment);
                await _dbContext.SaveChangesAsync();
                return comment.Id;
        }

        public async Task<bool> ProjectChangeStatusAsync(Guid id, int status)
        {
            var projectExist = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            var statusCodeIsValid = Enum.IsDefined(typeof(ProjectStatusEnum), status);

            if (statusCodeIsValid && projectExist is not null)
            {
                projectExist.UpdateStatus(status);
                _dbContext.Update(projectExist);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Project?> ProjectExistAsync(Guid idProject)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == idProject);
        }
    }
}
