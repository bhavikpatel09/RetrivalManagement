using RxWeb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.UnitOfWork
{
    public class BaseUow : CoreUnitOfWork
    {
        public BaseUow(IDbContext context, IRepositoryProvider repositoryProvider, IAuditLog auditLog = null) : base(context, repositoryProvider, auditLog) { }
    }
}
