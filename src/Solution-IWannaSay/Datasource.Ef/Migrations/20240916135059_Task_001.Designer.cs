﻿// <auto-generated />
using Datasource.Ef.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datasource.Ef.Migrations
{
    [DbContext(typeof(DbContextIWannaSay))]
    [Migration("20240916135059_Task_001")]
    partial class Task_001
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Datasource.Ef.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "Лев",
                            Name = "Лев Николаевич Мышкин",
                            Password = "1"
                        },
                        new
                        {
                            Id = 2,
                            Login = "Настасья",
                            Name = "Настасья Филипповна Барашкова",
                            Password = "1"
                        },
                        new
                        {
                            Id = 3,
                            Login = "Парфен",
                            Name = "Парфён Семёнович Рогожин",
                            Password = "1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
