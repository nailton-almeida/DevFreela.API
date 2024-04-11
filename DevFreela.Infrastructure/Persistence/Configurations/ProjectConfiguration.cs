using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasKey(e => e.Id);


            builder
               .HasOne(e => e.Freelancer)
               .WithMany(e => e.FreelancerProjet)
               .HasForeignKey(e => e.IdFreelancer)
               .OnDelete(DeleteBehavior.Restrict);


            builder
             .HasOne(e => e.Client)
             .WithMany(e => e.OwnerProject)
             .HasForeignKey(e => e.IdClient)
             .OnDelete(DeleteBehavior.Restrict);


            builder
            .Property(p => p.TotalCost)
            .HasColumnType("decimal(18,2)");






        }
    }
}
