// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Organizator_Proslava.Data;

namespace Organizator_Proslava.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210608233334_changed statuts notificaiton")]
    partial class changedstatutsnotificaiton
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

            modelBuilder.Entity("Organizator_Proslava.Model.Celebration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<float>("BudgetFrom")
                        .HasColumnType("float");

                    b.Property<float>("BudgetTo")
                        .HasColumnType("float");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTimeFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateTimeTo")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ExpectedNumberOfGuests")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsBudgetFixed")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("OrganizerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Celebrations");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationId");

                    b.ToTable("CelebrationDetail");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CollaboratorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.ToTable("CelebrationHalls");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.Guest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DinningTableId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("PositionX")
                        .HasColumnType("double");

                    b.Property<double>("PositionY")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("DinningTableId");

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationHallId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Movable")
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

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.CelebrationProposal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationDetailId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CelebrationHallId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationResponseId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CollaboratorId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationDetailId");

                    b.HasIndex("CelebrationHallId");

                    b.HasIndex("CelebrationResponseId");

                    b.HasIndex("CollaboratorId");

                    b.ToTable("CelebrationProposals");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.CelebrationResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("CelebrationResponses");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.ProposalComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationProposalId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("WriterId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationProposalId");

                    b.HasIndex("WriterId");

                    b.ToTable("ProposalComments");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Cellebrations.CellebrationType", b =>
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

                    b.HasKey("Id");

                    b.ToTable("CellebrationTypes");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.CollaboratorService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CollaboratorServiceBookId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Unit")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorServiceBookId");

                    b.ToTable("CollaboratorServices");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.CollaboratorServiceBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CollaboratorId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId")
                        .IsUnique();

                    b.ToTable("CollaboratorServiceBooks");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CelebrationResponseId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("ForUserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CelebrationResponseId");

                    b.ToTable("Notifications");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Notification");
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

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.Collaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Images")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("Collaborator_PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasIndex("AddressId");

                    b.HasDiscriminator().HasValue("Collaborator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Organizer", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.BaseUser");

                    b.Property<Guid>("AddressId")
                        .HasColumnName("Organizer_AddressId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CellebrationTypeId")
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

                    b.HasIndex("CellebrationTypeId");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.DinningTable", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

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

            modelBuilder.Entity("Organizator_Proslava.Model.NewCommentNotification", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Notification");

                    b.Property<int>("NumOfComments")
                        .HasColumnType("int");

                    b.Property<Guid>("ProposalId")
                        .HasColumnType("char(36)");

                    b.HasIndex("ProposalId");

                    b.HasDiscriminator().HasValue("NewCommentNotification");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.NewDetailNotification", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Notification");

                    b.Property<Guid>("DetailId")
                        .HasColumnType("char(36)");

                    b.HasIndex("DetailId");

                    b.HasDiscriminator().HasValue("NewDetailNotification");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.NewProposalNotification", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Notification");

                    b.Property<Guid>("ProposalId")
                        .HasColumnName("NewProposalNotification_ProposalId")
                        .HasColumnType("char(36)");

                    b.HasIndex("ProposalId");

                    b.HasDiscriminator().HasValue("NewProposalNotification");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.IndividualCollaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Collaborators.Collaborator");

                    b.Property<string>("JMBG")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PersonalId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("IndividualCollaborator");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.LegalCollaborator", b =>
                {
                    b.HasBaseType("Organizator_Proslava.Model.Collaborators.Collaborator");

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

            modelBuilder.Entity("Organizator_Proslava.Model.Celebration", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Organizator_Proslava.Model.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("Organizator_Proslava.Model.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId");
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationDetail", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Celebration", null)
                        .WithMany("CelebrationDetails")
                        .HasForeignKey("CelebrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Collaborators.Collaborator", "Collaborator")
                        .WithMany("CelebrationHalls")
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.Guest", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationHalls.DinningTable", "DinningTable")
                        .WithMany("Guests")
                        .HasForeignKey("DinningTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationHalls.PlaceableEntity", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", null)
                        .WithMany("PlaceableEntities")
                        .HasForeignKey("CelebrationHallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.CelebrationProposal", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationDetail", "CelebrationDetail")
                        .WithMany()
                        .HasForeignKey("CelebrationDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Organizator_Proslava.Model.CelebrationHalls.CelebrationHall", "CelebrationHall")
                        .WithMany()
                        .HasForeignKey("CelebrationHallId");

                    b.HasOne("Organizator_Proslava.Model.CelebrationResponses.CelebrationResponse", "CelebrationResponse")
                        .WithMany("CelebrationProposals")
                        .HasForeignKey("CelebrationResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Organizator_Proslava.Model.Collaborators.Collaborator", "Collaborator")
                        .WithMany()
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.CelebrationResponse", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Celebration", "Celebration")
                        .WithMany()
                        .HasForeignKey("CelebrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Organizator_Proslava.Model.Organizer", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.CelebrationResponses.ProposalComment", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationResponses.CelebrationProposal", "CelebrationProposal")
                        .WithMany("ProposalComments")
                        .HasForeignKey("CelebrationProposalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Organizator_Proslava.Model.BaseUser", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.CollaboratorService", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Collaborators.CollaboratorServiceBook", null)
                        .WithMany("Services")
                        .HasForeignKey("CollaboratorServiceBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.CollaboratorServiceBook", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Collaborators.Collaborator", null)
                        .WithOne("CollaboratorServiceBook")
                        .HasForeignKey("Organizator_Proslava.Model.Collaborators.CollaboratorServiceBook", "CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Notification", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationResponses.CelebrationResponse", "CelebrationResponse")
                        .WithMany()
                        .HasForeignKey("CelebrationResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Collaborators.Collaborator", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.Organizer", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Organizator_Proslava.Model.Cellebrations.CellebrationType", "CellebrationType")
                        .WithMany()
                        .HasForeignKey("CellebrationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.NewCommentNotification", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationResponses.CelebrationProposal", "Proposal")
                        .WithMany()
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.NewDetailNotification", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationDetail", "Detail")
                        .WithMany()
                        .HasForeignKey("DetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Organizator_Proslava.Model.NewProposalNotification", b =>
                {
                    b.HasOne("Organizator_Proslava.Model.CelebrationResponses.CelebrationProposal", "Proposal")
                        .WithMany()
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
