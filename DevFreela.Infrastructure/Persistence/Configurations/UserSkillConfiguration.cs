using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder
            .HasKey(p => p.Id);


            builder
           .HasOne(e => e.User)
           .WithMany()
           .HasForeignKey(p => p.IdUser);

            builder
                .HasOne(e => e.Skill)
                .WithMany()
                .HasForeignKey(p => p.IdSkill);



        }
    }
}
