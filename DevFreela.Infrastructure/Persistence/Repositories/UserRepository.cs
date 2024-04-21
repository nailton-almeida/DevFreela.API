using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> EditUserAsync(EditUserCommand infoUpdated)
        {

            var editUser = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == infoUpdated.Id);
            if (editUser is not null)
            {
                editUser.UpdateUser(infoUpdated.Fullname, infoUpdated.Email, infoUpdated.Birthday);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            var loginIsValid = await _dbContext.Users.SingleOrDefaultAsync(p => p.Email == email && p.Password == password);
            return loginIsValid;
        }

        public async Task<User?> UsersExistAndActivateAsync(int userId)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == userId && p.IsActive == true);
        }

        public async Task<bool> UsersHasProjectAsync(int userId)
        {
            var userExist = await UsersExistAndActivateAsync(userId);

            if (userExist is not null)
            {
                return await _dbContext.Projects.AnyAsync(p => (p.IdClient == userId || p.IdFreelancer == userId));
            }

            return false;

        }
    }
}
