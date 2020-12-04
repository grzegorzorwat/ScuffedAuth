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
            builder.Entity<Client>().Property(p => p.Secret).IsRequired().HasMaxLength(74);

            builder.Entity<Client>().HasData
            (
                //clientSecret
                new Client("clientId", "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw="),
                //8501994fb739294e2421ee40036ac7db
                new Client("c90c4832101ee1cf19c859e276527867", "1000.15qVsWgYLYz1X5qaNaF+Fg==.3jQRY0tAwXEJ7ulvfEx6+FJd0Q0w35b9BZNea9tmlI4=")
            );
        }
    }
}
