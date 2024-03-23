using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;

public class GetUserByIdQuery : IRequest<UsersViewModel>
{
    public int Id { get; set; }
}
