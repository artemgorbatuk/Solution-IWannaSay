using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datasource.Ef.Seeds;
public class SeedUser : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.HasData(new User {
            Id = 1,
            Name = "Лев Николаевич Мышкин",
            Login = "Лев",
            Password = "1"
        });
        builder.HasData(new User {
            Id = 2,
            Name = "Настасья Филипповна Барашкова",
            Login = "Настасья",
            Password = "1"
        });
        builder.HasData(new User {
            Id = 3,
            Name = "Парфён Семёнович Рогожин",
            Login = "Парфен",
            Password = "1"
        });
    }
}