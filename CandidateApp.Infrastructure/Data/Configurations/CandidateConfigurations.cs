using CandidateApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CandidateApp.Infrastructure.Data.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Email).IsUnique();
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.Comment).IsRequired().HasColumnType("nvarchar(max)");
        }
    }
}
