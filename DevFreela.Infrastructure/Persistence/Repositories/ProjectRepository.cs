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
        private readonly IUserRepository _userRepository;
        public ProjectRepository(DevFreelaDbContext dbContext, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
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
            
            var userExist = await _userRepository.UsersExistAndActivateAsync(id);

            if (userExist)
            {
                var projectByUser = _dbContext.Projects.Where(p => p.IdClient == id);
            }
            return null;

        }
        public async Task<Guid?> CreateProjectAsync(Project project)
        {
          await _dbContext.Projects.AddAsync(project);
          await _dbContext.SaveChangesAsync();
          return project.Id;
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
                _dbContext.ProjectComments.Add(comment);
                _dbContext.SaveChanges();
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

        public async Task<bool> ProjectExistAsync(Guid idProject, int idUser)
        {
            return await _dbContext.Projects.AnyAsync(p => p.Id == idProject && (p.IdClient == idUser || p.IdFreelancer == idUser));
        }
    }
}
