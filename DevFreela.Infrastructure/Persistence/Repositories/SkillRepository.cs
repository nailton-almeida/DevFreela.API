
using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Interfaces
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
                
        public async Task<IEnumerable<SkillsViewModel>> GetAllAsync()
        {
            return await _dbContext.Skills.Select(p => new SkillsViewModel(p.Id, p.Name, p.TypeSkill)).ToListAsync();
        }

        //public async Task<SkillsViewModel> GetByIdAsync(int id)
        //{
                        
        //    var skill = await _dbContext.Skills.SingleOrDefaultAsync(p => p.Id == id);
        //    if (skill != null) {
            
        //    return new SkillsViewModel
        //       (
        //        skill.Id,
        //        skill.Name,
        //        skill.TypeSkill

        //        );

        //    }
        //    return null;
        //}

        public async Task<int?> CreateAsync(SkillsInputModel skill)
        {
            var skillExist = await _dbContext.Skills.FirstOrDefaultAsync(p=> p.Name == skill.Name);
            
            if(skillExist == null)
            {
                var newSkill = new Skill(skill.Name, skill.TypeSkills);
                _dbContext.Skills.Add(newSkill);
                _dbContext.SaveChanges();
                return newSkill.Id;

            }


            return null;
                
              
        }
    }
}
