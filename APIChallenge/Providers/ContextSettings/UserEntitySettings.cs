using APIChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace APIChallenge.Providers.ContextSettings
{
    public class UserEntitySettings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelbuilder)
        {
            modelbuilder.ToTable("USER");

            modelbuilder.HasKey(u => u.Id);

            modelbuilder.Property(u => u.Id)
                .HasColumnName("USER_ID")
                .IsRequired();

            modelbuilder.Property(u => u.Password)
                 .HasColumnName("USER_PASSWORD")
                 .HasMaxLength(100)
                 .IsRequired();

            modelbuilder.Property(u => u.Email)
                 .HasColumnName("USER_EMAIL")
                 .HasMaxLength(100)
                 .IsRequired();

            modelbuilder.HasOne(s => s.Security).WithOne(u => u.User).HasForeignKey<Security>(u => u.Id).IsRequired();
        }
    }
}
