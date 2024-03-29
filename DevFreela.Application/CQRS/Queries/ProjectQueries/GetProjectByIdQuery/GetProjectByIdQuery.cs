using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;

public class GetProjectByIdQuery : IRequest<ProjectDetailsViewModel>
{
    public GetProjectByIdQuery(Guid id)
    {
        Id = id;
         
    }


    public Guid Id { get; set; }
   
}
