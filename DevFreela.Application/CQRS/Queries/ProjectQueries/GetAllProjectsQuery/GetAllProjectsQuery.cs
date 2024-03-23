using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery
{
    public class GetAllProjectsQuery : IRequest<List<ProjectViewModel>>
    {

    }
}
