﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Organizator_Proslava.Data;

namespace Organizator_Proslava.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210516150559_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Organizator_Proslava.Model.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Lat")
                        .HasColumnType("float");

                    b.Property<float>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("WholeAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.BaseUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MailAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("BaseUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseUser");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CelebrationHalls");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CelebrationHallId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("PositionX")
                        .HasColumnType("double");

                    b.Property<double>("PositionY")
                        .HasColumnType("double");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationHallId");

                    b.ToTable("PlaceableEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PlaceableEntity");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Administrator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Client", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("Collaborator_PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("Collaborator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Organizer", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.Property<Guid?>("AddressId")
                        .HasColumnName("Organizer_AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("JMBG")
                        .HasColumnName("Organizer_JMBG")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PersonalId")
                        .HasColumnName("Organizer_PersonalId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("Organizer_PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.DinningTable", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity");

                    b.HasDiscriminator().HasValue("DinningTable");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.Music", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity");

                    b.HasDiscriminator().HasValue("Music");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.ServingTable", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity");

                    b.HasDiscriminator().HasValue("ServingTable");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.IndividualCollaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Collaborator");

                    b.Property<string>("JMBG")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PersonalId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("IndividualCollaborator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.LegalCollaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Collaborator");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PIB")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("LegalCollaborator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.TableFor18", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.DinningTable");

                    b.HasDiscriminator().HasValue("TableFor18");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.TableFor6", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.DinningTable");

                    b.HasDiscriminator().HasValue("TableFor6");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", null)
                        .WithMany("PlaceableEntities")
                        .HasForeignKey("CelebrationHallId");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborator", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Organizer", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
