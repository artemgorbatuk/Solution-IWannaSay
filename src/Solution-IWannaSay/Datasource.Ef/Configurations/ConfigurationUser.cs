using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datasource.Ef.Configurations;
public class ConfigurationUser : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {

        builder.ToTable("Users", "dbo");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}