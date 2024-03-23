using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;

public class GetProjectByUserIdQuery : IRequest<List<ProjectViewModel>>
{
    public int Id { get; set; }
}
