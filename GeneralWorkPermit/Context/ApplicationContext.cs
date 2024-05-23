using GeneralWorkPermit.Configuration;
using GeneralWorkPermit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GeneralWorkPermit.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Applicants> applicant { get; set; }
        public DbSet<GasTestingRequireemnts> gastesting { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Admin> admins { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new DefaultUser());
            builder.ApplyConfiguration(new DefaultAdmin());
        }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite("DataSource = GeneralWorkPermit");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
