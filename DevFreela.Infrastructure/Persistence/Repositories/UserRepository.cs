using DevFreela.Core.Entities;
using DevFreela.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.ViewModels;
using DevFreela.Application.InputModel;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsersViewModel>> GetAllAsync()
        {
            return await _dbContext.Users.AsNoTracking().Select(p => new UsersViewModel(p.Id, p.Fullname, p.Email, p.CreatedAt, p.IsActive)).ToListAsync();
            
        }

        public async Task<UsersViewModel> GetByIdAsync(int id)
        {
            var userExist = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);

            if (userExist is not null)
            {
                var userViewModel = new UsersViewModel(

                     userExist.Id,
                     userExist.Fullname,
                     userExist.Email,
                     userExist.CreatedAt,
                     userExist.IsActive);

                return userViewModel;
            }

            return null;
        }


        public async Task<int?> CreateUserAsync(UsersInputModel userInput)
        {
            var userExist = await _dbContext.Users.AnyAsync(u => u.Email == userInput.Email);
            if (!userExist)
            {
                var userInputModel = new User(               
                    
                    userInput.Fullname,
                    userInput.Email,
                    userInput.Birthday
                    
                   
             ); 
                
                _dbContext.Users.Add(userInputModel);
                _dbContext.SaveChanges();
                return userInputModel.Id;
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
