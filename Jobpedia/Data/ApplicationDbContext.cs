using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Jobpedia.Models;

namespace Jobpedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Jobpedia.Models.Job>? Job { get; set; }
        public DbSet<Jobpedia.Models.Jobs>? Jobs { get; set; }
        public DbSet<Jobpedia.Models.Applications>? Applications { get; set; }
    }
}