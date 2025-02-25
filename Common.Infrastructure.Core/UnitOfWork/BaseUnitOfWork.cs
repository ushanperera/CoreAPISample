using Common.Domain.Core.Business;
using Common.Infrastructure.Base.Context;
using Common.Infrastructure.Core.Contracts;
using System.Threading.Tasks;

namespace Common.Infrastructure.Core.UnitOfWork
{
    public class BaseUnitOfWork: IBaseUnitOfWork
    {
        private readonly TenantDbContext context;

        public IGenericRepository<ApplicationArea> ApplicationAreas { get; }

        public BaseUnitOfWork(TenantDbContext context, 
            IGenericRepository<ApplicationArea> ApplicationAreas)
        {
            this.context = context;
            this.ApplicationAreas = ApplicationAreas;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            await context.BeginTransactionAsync();
            
        }

        public void CommitTransaction()
        {
            context.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            context.RollbackTransaction();
        }
    }
}
