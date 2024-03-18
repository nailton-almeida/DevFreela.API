using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentcsConfigurations : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasOne(e => e.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdProject);

            builder
                .HasOne(e => e.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.IdUser);

        }
    }
}
