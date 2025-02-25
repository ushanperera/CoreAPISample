using Common.Domain.Core.Business;
using Common.Infrastructure.Base.Context;
using Common.Infrastructure.Core.Repository;

namespace Common.Infrastructure.Repository
{
    public class AplicationAreaRepository : GenericRepository<ApplicationArea>, IAplicationAreaRepository
    {
        public AplicationAreaRepository(TenantDbContext context) : base(context) { }

        public TenantDbContext TenantDbContext => context as TenantDbContext;
    }
}
