using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;

public class GetProjectByUserIdQueryHandler : IRequestHandler<GetProjectByUserIdQuery, List<ProjectViewModel>>
{
    private readonly IProjectRepository _repository;

    public GetProjectByUserIdQueryHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProjectViewModel>?> Handle(GetProjectByUserIdQuery request, CancellationToken cancellationToken)
    {
        var projectList = await _repository.GetByUserIdAsync(request.Id);

        if (projectList == null) return null;

        return projectList.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();

    }
}
