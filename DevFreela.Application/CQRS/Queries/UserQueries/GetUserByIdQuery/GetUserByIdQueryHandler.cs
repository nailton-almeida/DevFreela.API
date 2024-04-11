using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UsersViewModel>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UsersViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is not null)
        {
            var userViewModel = new UsersViewModel(

                 user.Id,
                 user.Fullname,
                 user.Email,
                 user.CreatedAt,
                 user.IsActive);

            return userViewModel;
        }
        return null;
    }
}
