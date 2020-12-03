using Microsoft.EntityFrameworkCore;
using ScuffedAuth.Authorization.ClientCredentials;

namespace ScuffedAuth.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Client>().HasKey(p => p.Id);
            builder.Entity<Client>().Property(p => p.Id).IsRequired().HasMaxLength(32);
            builder.Entity<Client>().Property(p => p.Secret).IsRequired().HasMaxLength(32);

            builder.Entity<Client>().HasData
            (
                new Client("clientId", "clientSecret")
            );
        }
    }
}
