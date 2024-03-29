using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery
{
    public class GetAllUsersQuery : IRequest<List<UsersViewModel>>
    {
    }
}
