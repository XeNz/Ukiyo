using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Core.Entities;
using Ukiyo.Infrastructure.DAL.Identity;
using Z.EntityFramework.Plus;

namespace Ukiyo.Core
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) => (context as AppDbContext)?.AuditEntries.AddRange(audit.Entries);
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ApplicationUser> User { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return this.SaveChangesAsync(new Audit(), cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            return SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}