using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datasource.Ef.Configurations;

public class ConfigurationMessage : IEntityTypeConfiguration<Message> {
    public void Configure(EntityTypeBuilder<Message> builder) {

        builder.ToTable("Messages", "dbo");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Text)
            .IsRequired();

        builder.HasOne(m => m.User)
            .WithMany(u => u.Messages)       
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}