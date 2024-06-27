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
    [Migration("20240627230155_PasswordMigration")]
    partial class PasswordMigration
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
                        .HasColumnType("nvarchar(max)");

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
                            Id = new Guid("1fc30fde-8a23-4b23-ab26-8a6d1b048e4d"),
                            Level = "Uneducated"
                        },
                        new
                        {
                            Id = new Guid("5ac66ace-9f8f-402c-aecf-130b026af991"),
                            Level = "Class 7"
                        },
                        new
                        {
                            Id = new Guid("77a22883-3a59-426b-a295-6c413e79f666"),
                            Level = "Form 2"
                        },
                        new
                        {
                            Id = new Guid("72f466d4-feb3-4f76-a9c0-067e3e8e9c2f"),
                            Level = "Form 4"
                        },
                        new
                        {
                            Id = new Guid("7057bd05-66c6-4b63-a369-dc88de53a551"),
                            Level = "Form 6"
                        },
                        new
                        {
                            Id = new Guid("12e3b1f6-9f50-4600-9651-6da9b22762da"),
                            Level = "Diploma"
                        },
                        new
                        {
                            Id = new Guid("e53fcbaa-d6e8-41e2-aa4f-1ab97115c2e7"),
                            Level = "Bachelor's Degree"
                        },
                        new
                        {
                            Id = new Guid("507e90ae-a8bd-41da-848a-c839473451cb"),
                            Level = "Master's Degree"
                        },
                        new
                        {
                            Id = new Guid("2c0bef62-0c86-4177-b5ff-f97bb3a13803"),
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
                            Id = new Guid("d5b2368f-3a6e-49c2-8e04-4beea33edd79"),
                            GenderType = "Male"
                        },
                        new
                        {
                            Id = new Guid("07c3cbd6-e75a-4ad9-85d7-7031aff7daa2"),
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
                            Id = new Guid("629cfb32-2a12-4aa0-94cd-4b1d1903588c"),
                            Class = "Class A"
                        },
                        new
                        {
                            Id = new Guid("7746b399-c123-45db-b8f8-17f437a422a0"),
                            Class = "Class A1"
                        },
                        new
                        {
                            Id = new Guid("ec525e85-8079-43e3-9e0a-15677cc55f3a"),
                            Class = "Class A2"
                        },
                        new
                        {
                            Id = new Guid("4b0d5e3e-9198-4f0d-b563-8fba973938b4"),
                            Class = "Class A3"
                        },
                        new
                        {
                            Id = new Guid("103c62b6-61d1-4a06-84ef-95c4e3ff4534"),
                            Class = "Class B"
                        },
                        new
                        {
                            Id = new Guid("0925f2de-e0f8-4fc1-8b1a-63fc7aefe4ad"),
                            Class = "Class C"
                        },
                        new
                        {
                            Id = new Guid("2aa6dfc7-929a-4977-b179-87b9416e655f"),
                            Class = "Class C1"
                        },
                        new
                        {
                            Id = new Guid("0c0a8429-8079-4aaa-88cf-70163968aba5"),
                            Class = "Class C2"
                        },
                        new
                        {
                            Id = new Guid("7f556ec1-b9ed-4c87-ad9b-ade83b51a479"),
                            Class = "Class C3"
                        },
                        new
                        {
                            Id = new Guid("1d43be8f-6479-4cf4-88f0-a35cb0981e5a"),
                            Class = "Class D"
                        },
                        new
                        {
                            Id = new Guid("0f24b0bf-8a01-4386-9339-b587d8a3896c"),
                            Class = "Class E"
                        },
                        new
                        {
                            Id = new Guid("b8c62c96-5d53-4751-bed9-77347c78c0be"),
                            Class = "Class F"
                        },
                        new
                        {
                            Id = new Guid("9c03fad4-c9c0-4569-8ac5-5e913d2a443b"),
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
                            Id = new Guid("3a62fb56-dc46-4c9b-9a9e-c159e75f14d2"),
                            Name = "SuperUser"
                        },
                        new
                        {
                            Id = new Guid("91b884f2-3f3f-4c29-8c79-744375cf90c5"),
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("32e75fcd-2d81-49c4-b2c0-cee0982af071"),
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
                        .HasColumnType("nvarchar(max)");

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
