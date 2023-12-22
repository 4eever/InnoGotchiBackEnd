﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231114210810_DeleteNulls")]
    partial class DeleteNulls
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccessLayer.Entities.DeadInnogotchi", b =>
                {
                    b.Property<int>("DeadInnogotchiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeadInnogotchiId"));

                    b.Property<int>("DeadInnogotchiAge")
                        .HasColumnType("int");

                    b.Property<int>("DeadInnogotchiName")
                        .HasColumnType("int");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.HasKey("DeadInnogotchiId");

                    b.HasIndex("FarmId");

                    b.ToTable("DeadInnogotchis");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Farm", b =>
                {
                    b.Property<int>("FarmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FarmId"));

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PetsAlive")
                        .HasColumnType("int");

                    b.Property<int>("PetsDead")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FarmId");

                    b.HasIndex("FarmName")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Innogotchi", b =>
                {
                    b.Property<int>("InnogotchiId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InnogotchiId"));

                    b.Property<int>("BodyNumber")
                        .HasColumnType("int");

                    b.Property<int>("DrinkCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DrintLastTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("EyesNumber")
                        .HasColumnType("int");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<int>("FedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("FedLastTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("HappinessDays")
                        .HasColumnType("int");

                    b.Property<string>("InnogotchiName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MouthNumber")
                        .HasColumnType("int");

                    b.Property<int>("NoseNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("PetDOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("SumDrinkPeriods")
                        .HasColumnType("int");

                    b.Property<int>("SumFedPeriods")
                        .HasColumnType("int");

                    b.HasKey("InnogotchiId");

                    b.HasIndex("FarmId");

                    b.HasIndex("InnogotchiName")
                        .IsUnique();

                    b.ToTable("Innogotchis");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte[]>("UserAvatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.UserFarm", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("FarmId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "FarmId");

                    b.HasIndex("FarmId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserFarms");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.DeadInnogotchi", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Farm", "Farm")
                        .WithMany("DeadInnogotchis")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Farm", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.User", "User")
                        .WithOne("Farm")
                        .HasForeignKey("DataAccessLayer.Entities.Farm", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Innogotchi", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Farm", "Farm")
                        .WithMany("Innogotchis")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.UserFarm", b =>
                {
                    b.HasOne("DataAccessLayer.Entities.Farm", "Farm")
                        .WithMany("UserFarms")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.Role", "Role")
                        .WithMany("UserFarms")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Entities.User", "User")
                        .WithMany("FarmUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Farm", b =>
                {
                    b.Navigation("DeadInnogotchis");

                    b.Navigation("Innogotchis");

                    b.Navigation("UserFarms");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.Role", b =>
                {
                    b.Navigation("UserFarms");
                });

            modelBuilder.Entity("DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("Farm");

                    b.Navigation("FarmUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
