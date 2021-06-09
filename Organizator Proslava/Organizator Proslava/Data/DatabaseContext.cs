using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Organizator_Proslava.Data
{
    public class DatabaseContext : DbContext
    {
        // Users

        public DbSet<BaseUser> BaseUsers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        // Placeable entities

        public DbSet<CelebrationHall> CelebrationHalls { get; set; }
        public DbSet<PlaceableEntity> PlaceableEntities { get; set; }
        public DbSet<DinningTable> DinningTables { get; set; }
        public DbSet<TableFor6> TablesFor6 { get; set; }
        public DbSet<TableFor18> TablesFor18 { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<ServingTable> ServingTables { get; set; }

        // Cellebrations

        public DbSet<CellebrationType> CellebrationTypes { get; set; }
        public DbSet<Celebration> Celebrations { get; set; }

        public DbSet<CelebrationResponse> CelebrationResponses { get; set; }
        public DbSet<CelebrationProposal> CelebrationProposals { get; set; }
        public DbSet<ProposalComment> ProposalComments { get; set; }

        // Collaborators

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<IndividualCollaborator> IndividualCollaborators { get; set; }
        public DbSet<LegalCollaborator> LegalCollaborators { get; set; }
        public DbSet<CollaboratorServiceBook> CollaboratorServiceBooks { get; set; }
        public DbSet<CollaboratorService> CollaboratorServices { get; set; }

        // Notification

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NewCommentNotification> NewCommentNotifications { get; set; }
        public DbSet<NewDetailNotification> NewDetailNotifications { get; set; }
        public DbSet<NewProposalNotification> NewProposalNotifications { get; set; }
        public DbSet<ChangedStatusOfProposalNotification> ChangedStatusOfProposalNotification { get; set; }
        public DbSet<CanceledResponseNotification> CanceledResponseNotifications { get; set; }
        public DbSet<CanceledCelebrationNotification> CanceledCelebrationNotifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = $"server=bjelicaluka.com;port=3310;database=hci;user=root;password=1234";
            optionsBuilder.EnableSensitiveDataLogging()
                .UseLazyLoadingProxies()
                .UseMySql(connectionString, b => b.MigrationsAssembly("Organizator Proslava"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collaborator>().Property(c => c.Images)
                .HasConversion(
                    list => JsonSerializer.Serialize(list, default),
                    list => JsonSerializer.Deserialize<List<string>>(list, default)
                );

            modelBuilder.Entity<Collaborator>()
                .HasMany(c => c.CelebrationHalls)
                .WithOne(ch => ch.Collaborator)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}