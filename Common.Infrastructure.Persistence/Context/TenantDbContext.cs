using Camms.Common.Domain.Core.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Camms.Common.Infrastructure.Persistence.Context
{
    public class TenantDbContext : DbContext
    {
        private readonly HttpContext _httpContext;
        private readonly IHttpContextAccessor _contextAccessor;

        //public Guid TenantId => new Guid("f4df5d41-c33f-4280-a139-39b01bf6f8ef");
        //public long? OrganizationProvider { get; set; }
        //public long? UserProvider { get; set; }
        //public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.Now;


        public TenantDbContext() : base() 
        { }

        public TenantDbContext(DbContextOptions<TenantDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var body = _contextAccessor;
            optionsBuilder.UseSqlServer(@"Data Source=CML-LAHIRUGAM\SQL2019;Initial Catalog=Auto_Project;Persist Security Info=True; Integrated Security=False;Trusted_Connection=False;User ID=sa;Password=123");
        }

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            await Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }

        private void TrackChanges()
        {
            foreach (var entity in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                // Logic to track of all the changes
            }
        }

        public DbSet<ApplicationArea> ApplicationArea { get; set; }
        //public DbSet<ModeB> ModelBs { get; set; }

    }
}
