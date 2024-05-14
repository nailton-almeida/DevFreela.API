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

        public async Task<List<Skill>> GetAllAsync()
        {
            return await _dbContext.Skills.AsNoTracking().ToListAsync();
        }

        public async Task<Skill?> CreateAsync(Skill skill)
        {
            var skillExist = await _dbContext.Skills.FirstOrDefaultAsync(p => p.Name == skill.Name);

            if (skillExist == null)
            {
                await _dbContext.Skills.AddAsync(skill);
                await _dbContext.SaveChangesAsync();
                return skill;

            }
            return null;

        }
    }
}
