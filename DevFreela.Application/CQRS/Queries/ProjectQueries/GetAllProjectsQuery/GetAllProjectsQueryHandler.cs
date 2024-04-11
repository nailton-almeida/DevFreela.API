using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectList = await _projectRepository.GetAllAsync();
            var projectViewMode = projectList.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();
            return projectViewMode;



        }

    }
}
