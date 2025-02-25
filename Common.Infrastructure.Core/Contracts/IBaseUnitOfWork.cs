using Common.Domain.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Core.Contracts
{
    public interface IBaseUnitOfWork
    {
        public IGenericRepository<ApplicationArea> ApplicationAreas { get; }

        Task CompleteAsync();
        void Complete();
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
