using RetrivalManagement.Models;
using RxWeb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.UnitOfWork.Login
{
    public class LoginUow : BaseUow, ILoginUow
    {
        public LoginUow(IRetrivalManagementDbContext context, IRepositoryProvider repositoryProvider) : base(context, repositoryProvider) { }
    }

    public interface ILoginUow : ICoreUnitOfWork { }
}
