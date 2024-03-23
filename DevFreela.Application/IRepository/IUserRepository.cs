using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.InputModel;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int?> CreateUserAsync(User user);
        Task<bool> EditUserAsync(int id, UsersInputModel user);
        Task<bool> InactiveUserAsync(int id);
    


        #region to do later
        //Task Login(int id);
        //Task ResetPassword(LoginModel login);
        #endregion
    }
}
