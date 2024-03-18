using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace DevFreela.Infrastructure.Persistence.Configurations;

public class ProjectSkillConfiguration : IEntityTypeConfiguration<ProjectSkill>
{
     
     

    public void Configure(EntityTypeBuilder<ProjectSkill> builder)
    {
        builder
        .HasKey(p => p.Id);

        builder
           .HasOne(e => e.Project)
           .WithMany()
           .HasForeignKey(p => p.IdProject);

        builder
            .HasOne(e => e.Skill)
            .WithMany()
            .HasForeignKey(p => p.IdSkill);

    }






}


