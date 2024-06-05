﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RSAllies.Users.Data;

#nullable disable

namespace RSAllies.Users.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20240604233616_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RSAllies.Users.Entities.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Administrators", "Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.EducationLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels", "Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c0f424e-89c0-4b59-ab2c-b45fa44a9c5c"),
                            Level = "Uneducated"
                        },
                        new
                        {
                            Id = new Guid("a0770ebc-12e9-48f9-90d9-9b5cfc594e31"),
                            Level = "Class 7"
                        },
                        new
                        {
                            Id = new Guid("5f51730c-518b-4aa2-8cd8-c61da2517585"),
                            Level = "Form 2"
                        },
                        new
                        {
                            Id = new Guid("9efeba97-bb9a-48b3-af07-803e79a4f800"),
                            Level = "Form 4"
                        },
                        new
                        {
                            Id = new Guid("ef3cd9db-b8cf-4faf-bfbb-ec10f0fe99b0"),
                            Level = "Form 6"
                        },
                        new
                        {
                            Id = new Guid("5449d74f-30a8-4439-9568-79d9e58861c5"),
                            Level = "Diploma"
                        },
                        new
                        {
                            Id = new Guid("7751497b-7924-42cf-8dca-3d44b89d7ce5"),
                            Level = "Bachelor's Degree"
                        },
                        new
                        {
                            Id = new Guid("c8b60d80-54c3-4a19-bef6-58ad0ea2e9fa"),
                            Level = "Master's Degree"
                        },
                        new
                        {
                            Id = new Guid("414c1743-2fd5-416b-a442-3bbc9a620aef"),
                            Level = "PHD"
                        });
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GenderType")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.ToTable("Genders", "Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ee2ca06a-6f3c-4d91-8bff-f3f8eb4a7935"),
                            GenderType = "Male"
                        },
                        new
                        {
                            Id = new Guid("fd1873e0-cf3e-4c82-875c-273e96d24828"),
                            GenderType = "Female"
                        });
                });

            modelBuilder.Entity("RSAllies.Users.Entities.LicenseClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("LicenseClasses", "Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a9a6209-2f0a-4820-8bdb-d51509de26f1"),
                            Class = "Class A"
                        },
                        new
                        {
                            Id = new Guid("c2894fe3-419d-4548-ad53-f2b11f209167"),
                            Class = "Class A1"
                        },
                        new
                        {
                            Id = new Guid("59c5fb75-8975-4202-a47f-0cde3096e8d3"),
                            Class = "Class A2"
                        },
                        new
                        {
                            Id = new Guid("13aef938-5b37-4bab-8dd4-c37583d9a8ff"),
                            Class = "Class A3"
                        },
                        new
                        {
                            Id = new Guid("dea89baa-d4cc-4969-bf52-01e3113c2be7"),
                            Class = "Class B"
                        },
                        new
                        {
                            Id = new Guid("ee9aa063-7b60-492c-9089-020845e92b95"),
                            Class = "Class C"
                        },
                        new
                        {
                            Id = new Guid("ca0e9c3e-ef35-46fc-8c44-49e1a2ac7326"),
                            Class = "Class C1"
                        },
                        new
                        {
                            Id = new Guid("c81f325d-3d31-41ac-8545-53b2f63c1aa5"),
                            Class = "Class C2"
                        },
                        new
                        {
                            Id = new Guid("76aaf2b9-69ce-447a-a2e5-8c52ce8f15cd"),
                            Class = "Class C3"
                        },
                        new
                        {
                            Id = new Guid("516cc8d3-979b-41e3-8e87-6670c4723a17"),
                            Class = "Class D"
                        },
                        new
                        {
                            Id = new Guid("13142fca-8d24-48db-9947-f63c092e5314"),
                            Class = "Class E"
                        },
                        new
                        {
                            Id = new Guid("47324fb5-3e99-4e85-aa21-6b29d5156c02"),
                            Class = "Class F"
                        },
                        new
                        {
                            Id = new Guid("3c530344-75cd-4a0c-bf7f-3dc09fc2b9a9"),
                            Class = "Class G"
                        });
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Nationality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nationalities", "Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles", "Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("89cf538f-db69-4442-bff8-99acc0bd1e8c"),
                            Name = "SuperUser"
                        },
                        new
                        {
                            Id = new Guid("8dd927de-1ccf-4582-9519-73b577855175"),
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("30c317cf-05ce-480a-9349-0dfcfc30e150"),
                            Name = "Manager"
                        });
                });

            modelBuilder.Entity("RSAllies.Users.Entities.SupportCase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CaseNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SupportCases", "Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EducationLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("GenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsForeigner")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("LicenseClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("NationalityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EducationLevelId");

                    b.HasIndex("GenderId");

                    b.HasIndex("Identification")
                        .IsUnique();

                    b.HasIndex("LicenseClassId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("FirstName", "MiddleName", "LastName")
                        .IsUnique();

                    b.ToTable("Users", "Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Accounts", "Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Administrator", b =>
                {
                    b.HasOne("RSAllies.Users.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.SupportCase", b =>
                {
                    b.HasOne("RSAllies.Users.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.User", b =>
                {
                    b.HasOne("RSAllies.Users.Entities.EducationLevel", "EducationLevel")
                        .WithMany("Users")
                        .HasForeignKey("EducationLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RSAllies.Users.Entities.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RSAllies.Users.Entities.LicenseClass", "LicenseClass")
                        .WithMany("Users")
                        .HasForeignKey("LicenseClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RSAllies.Users.Entities.Nationality", null)
                        .WithMany("Users")
                        .HasForeignKey("NationalityId");

                    b.Navigation("EducationLevel");

                    b.Navigation("Gender");

                    b.Navigation("LicenseClass");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.UserAccount", b =>
                {
                    b.HasOne("RSAllies.Users.Entities.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("RSAllies.Users.Entities.UserAccount", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.EducationLevel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Gender", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.LicenseClass", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.Nationality", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RSAllies.Users.Entities.User", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
