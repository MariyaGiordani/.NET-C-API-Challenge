using APIChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIChallenge.Providers.ContextSettings
{
    public class SecurityEntitySettings : IEntityTypeConfiguration<Security>
    {
        public void Configure(EntityTypeBuilder<Security> modelbuilder)
        {
            modelbuilder.ToTable("SECURITY");

            modelbuilder.HasKey(s => s.Id);

            modelbuilder.Property(s => s.SaltPassword)
                 .HasColumnName("SALT_PASSWORD")
                 .IsRequired();

            modelbuilder.Property(s => s.Id)
                 .HasColumnName("USER_ID")
                 .IsRequired();
        }
    }
}
