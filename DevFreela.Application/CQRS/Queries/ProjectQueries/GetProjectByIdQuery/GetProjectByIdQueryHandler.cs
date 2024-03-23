using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;

public class GetProjectByIdQueryhandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
{
    private readonly IProjectRepository _repository;
    public GetProjectByIdQueryhandler(IProjectRepository repository)
    {
        _repository = repository;
    }
    public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var projectSelected = await _repository.GetByIdAsync(request.Id);

        if (projectSelected == null) return null;

        return new ProjectDetailsViewModel(

                   projectSelected.Id,
                   projectSelected.Title,
                   projectSelected.Description,
                   projectSelected.TotalCost,
                   projectSelected.StartedAt,
                   projectSelected.FinishedAt,
                   projectSelected.Client.Fullname,
                   projectSelected.Freelancer.Fullname

       );
    }
}
