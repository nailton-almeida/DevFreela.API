using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int?> CreateUserAsync(User user);
        Task<bool> EditUserAsync(EditUserCommand editedInfo);
        Task<bool> InactiveUserAsync(int id);
    
    }
}
