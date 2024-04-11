using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery
{

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UsersViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UsersViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetAllAsync();
            var userView = userList.Select(user => new UsersViewModel(user.Id, user.Fullname, user.Email, user.CreatedAt, user.IsActive)).ToList();
            return userView;
        }
    }
}
