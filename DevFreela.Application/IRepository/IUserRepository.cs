﻿using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User?> LoginUserAsync(string email, string password);
        Task<int?> CreateUserAsync(User user);
        Task<bool> EditUserAsync(EditUserCommand editedInfo);
        Task<bool> ChangePassword(string newPassword);
        Task<bool> InactiveUserAsync(int id);
        Task<User?> UsersExistAndActivateAsync(int userId);
        Task<bool> UsersHasProjectAsync(int userId);

    }
}
