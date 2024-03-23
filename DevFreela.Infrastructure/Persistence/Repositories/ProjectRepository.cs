using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.InputModel;
using DevFreela.Application.InputModels;
using DevFreela.Core.Enums;


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

        //public async Task<IEnumerable<ProjectViewModel>> GetBySkill(List<string> skillsNames)
        //{


        //    throw new NotImplementedException();
        //}

        public async Task<Guid?> CreateProjectAsync(NewProjectInputModel inputModel)
        {

            var clientExist = await _dbContext.Users.AnyAsync(p => p.Id == inputModel.ClientID);
            var freelancerExist = await _dbContext.Users.AnyAsync(p => p.Id == inputModel.FreelancerID);

            if (clientExist && freelancerExist)
            {
                var projectInput = new Project(

                //Id = new Guid(),
                inputModel.Title,
                inputModel.Description,
                inputModel.ClientID,
                inputModel.FreelancerID,
                inputModel.TotalCost,
                inputModel.StartedAt,
                inputModel.FinishedAt);

                _dbContext.Projects.Add(projectInput);
                _dbContext.SaveChanges();
                return projectInput.Id;

            }
            return null;

        }

        public async Task<bool> UpdateProjectAsync(Guid id, UpdateProjectInputModel project)
        {
            var projectExist = await _dbContext.Projects.AnyAsync(p => p.Id == id);
            if (projectExist)
            {
                var updateProject = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
                updateProject.UpdateProject(project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<bool> PostComentsAsync(Guid id, CreateCommentInputModelcs commentary)
        {


            var projectExist = await _dbContext.Projects.AnyAsync(p => p.Id == id && (p.IdClient == commentary.IdUser || p.IdFreelancer == commentary.IdUser));
            if (projectExist)
            {
                var newComment = new ProjectComment(commentary.Comment, id, commentary.IdUser);

                _dbContext.ProjectComments.Add(newComment);
                _dbContext.SaveChanges();
                return true;
            }
            return false;


        }

        public async Task<bool> ProjectChangeStatusAsync(Guid id, int status)
        {
            var projectExist = await _dbContext.Projects.AnyAsync(p => p.Id == id);
            var statusCodeIsValid = Enum.IsDefined(typeof(ProjectStatusEnum), status);

            if (statusCodeIsValid && projectExist)
            {

                var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
                project.UpdateStatus(status);
                _dbContext.Update(project);
                _dbContext.SaveChanges();
                return true;

            }
            return false;

        }


    }
}
