using RetrivalManagement.Models;
using RxWeb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.UnitOfWork.UserManagement
{
    public class UserUow : BaseUow, IUserUow
    {
        public UserUow(IRetrivalManagementDbContext context, IRepositoryProvider repositoryProvider) : base(context, repositoryProvider) { }
    }

    public interface IUserUow : ICoreUnitOfWork { }
}
