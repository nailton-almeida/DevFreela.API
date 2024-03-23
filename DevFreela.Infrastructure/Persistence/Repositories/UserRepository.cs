using DevFreela.Core.Entities;
using DevFreela.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.ViewModels;
using DevFreela.Application.InputModel;
using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.AsNoTracking().ToListAsync();            
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
           
        }


        public async Task<int?> CreateUserAsync(User user)
        {
            var userExist = await _dbContext.Users.AnyAsync(u => u.Email == user.Email);
            if (!userExist)
            {
                               
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user.Id;
            }

            return null;
        }

        public async Task<bool> InactiveUserAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id && u.IsActive == true);
            if (user != null)
            {
                user.InactiveUser(false);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> EditUserAsync(int id, UsersInputModel userinput)
        {

            var editUser = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
            if (editUser is not null)
            {
                editUser.UpdateUser(userinput.Fullname, userinput.Email, userinput.Birthday);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
             

           
        }

         
    }
}
