using Identity.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.DAL
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Initial Catalog=Identity;User ID=SA;Password=Password_123");
            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
