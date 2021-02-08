using Microsoft.EntityFrameworkCore;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; } = default!;
        public DbSet<TokenEntity> Tokens { get; set; } = default!;
        public DbSet<AuthorizationCodeEntity> AuthorizationCodes { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientEntity>().ToTable("Clients");
            builder.Entity<ClientEntity>().HasKey(p => p.Id);
            builder.Entity<ClientEntity>().Property(p => p.Id).IsRequired().HasMaxLength(32);
            builder.Entity<ClientEntity>().Property(p => p.Secret).IsRequired().HasMaxLength(74);
            builder.Entity<ClientEntity>().Property(p => p.RedirectUri).HasMaxLength(200);

            builder.Entity<ClientEntity>().HasData
            (
                new ClientEntity
                {
                    Id = "clientId",
                    //clientSecret
                    Secret = "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw="
                },
                new ClientEntity
                {
                    Id = "c90c4832101ee1cf19c859e276527867",
                    //8501994fb739294e2421ee40036ac7db
                    Secret = "1000.15qVsWgYLYz1X5qaNaF+Fg==.3jQRY0tAwXEJ7ulvfEx6+FJd0Q0w35b9BZNea9tmlI4="
                }
            );

            builder.Entity<TokenEntity>().ToTable("Tokens");
            builder.Entity<TokenEntity>().HasKey(p => p.Value);
            builder.Entity<TokenEntity>().Property(p => p.Value).IsRequired().HasMaxLength(32);
            builder.Entity<TokenEntity>().Property(p => p.CreationDate).IsRequired();
            builder.Entity<TokenEntity>().Property(p => p.ExpiresIn).IsRequired();
            builder.Entity<TokenEntity>().Property(p => p.TokenType).IsRequired().HasMaxLength(100);

            builder.Entity<AuthorizationCodeEntity>().ToTable("AuthorizationCodes");
            builder.Entity<AuthorizationCodeEntity>().HasKey(p => p.Code);
            builder.Entity<AuthorizationCodeEntity>().Property(p => p.Code).IsRequired().HasMaxLength(32);
            builder.Entity<AuthorizationCodeEntity>().Property(p => p.CreationDate).IsRequired();
            builder.Entity<AuthorizationCodeEntity>().Property(p => p.ExpiresIn).IsRequired();
            builder.Entity<AuthorizationCodeEntity>().Property(p => p.ClientId).IsRequired().HasMaxLength(32);
            builder.Entity<AuthorizationCodeEntity>().Property(p => p.RedirectUri).HasMaxLength(200);
        }
    }
}
