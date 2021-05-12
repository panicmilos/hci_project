using Microsoft.EntityFrameworkCore;
using Organizator_Proslava.Model;

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
    }
}