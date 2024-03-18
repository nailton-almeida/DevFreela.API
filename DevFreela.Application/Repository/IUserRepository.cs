using DevFreela.Application.InputModel;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsersViewModel>> GetAllAsync();
        Task<UsersViewModel> GetByIdAsync(int id);
        Task<int?> CreateUserAsync(UsersInputModel user);
        Task<bool> EditUserAsync(int id, UsersInputModel user);
        Task<bool> InactiveUserAsync(int id);
       

        #region to do later
        //Task Login(int id);
        //Task ResetPassword(LoginModel login);
        #endregion
    }
}
