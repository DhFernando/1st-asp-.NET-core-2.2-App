﻿// <auto-generated />
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagementSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200329132445_AddPhotoPathColumn")]
    partial class AddPhotoPathColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeManagementSystem.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Department");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("MyProperty");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Department = 3,
                            Email = "hasitha@gmail.com",
                            Name = "Hasitha"
                        },
                        new
                        {
                            Id = 4,
                            Department = 2,
                            Email = "Fernando@gmail.com",
                            Name = "Fernando"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
