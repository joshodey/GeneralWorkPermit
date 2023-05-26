using GeneralWorkPermit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeneralWorkPermit.Context
{
    public class ApplicationContext : IdentityDbContext<Applicants>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {}

        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Applicants> applicant { get; set; }
        public DbSet<GasTestingRequireemnts> gastesting { get; set; }
    }
}
