using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;

public class GetUserByIdQuery : IRequest<UsersViewModel>
{
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
