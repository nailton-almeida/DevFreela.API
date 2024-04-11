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
        public ProjectRepository(DevFreelaDbContext dbContext)
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
            var userExist = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (userExist is not null)
            {
                var projectByUser = _dbContext.Projects.Where(p => p.IdClient == id);
            }
            return null;

        }
        public async Task<Guid?> CreateProjectAsync(Project project)
        {

            var clientExist = await _dbContext.Users.AnyAsync(p => p.Id == project.IdClient);
            var freelancerExist = await _dbContext.Users.AnyAsync(p => p.Id == project.IdFreelancer);

            if (clientExist && freelancerExist)
            {
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
                return project.Id;
            }
            return null;

        }

        public async Task<Guid?> UpdateProjectAsync(UpdateProjectCommand project)
        {
            var projectExist = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == project.Id);

            var isValidStatus = (int)projectExist.Status == 4 || (int)projectExist.Status == 5;

            if (projectExist is not null && isValidStatus)
            {
                projectExist.UpdateProject(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt);
                await _dbContext.SaveChangesAsync();
                return projectExist.Id;
            }

            return null;
        }


        public async Task<Guid?> PostComentsAsync(ProjectComment comment)
        {

            var projectExist = await _dbContext.Projects.AnyAsync(p => p.Id == comment.IdProject && (p.IdClient == comment.IdUser || p.IdFreelancer == comment.IdUser));
            if (projectExist)
            {
                _dbContext.ProjectComments.Add(comment);
                _dbContext.SaveChanges();
                return comment.Id;
            }
            return null;


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


    }
}
