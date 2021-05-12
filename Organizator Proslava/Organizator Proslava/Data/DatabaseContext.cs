using Microsoft.EntityFrameworkCore;
using Organizator_Proslava.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Organizator_Proslava.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BaseUser> BaseUsers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<IndividualCollaborator> IndividualCollaborators { get; set; }
        public DbSet<LegalCollaborator> LegalCollaborators { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = $"server=bjelicaluka.com;port=3310;database=hci;user=root;password=1234";
            optionsBuilder.UseMySql(connectionString, b => b.MigrationsAssembly("Organizator Proslava"));
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsActive"] = false;
                        break;
                }
            }
        }
    }
}