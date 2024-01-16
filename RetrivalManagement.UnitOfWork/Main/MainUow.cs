using RetrivalManagement.Models;
using RxWeb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.UnitOfWork.Main
{
    public class MainUow : BaseUow, IMainUow
    {
        public MainUow(IRetrivalManagementDbContext context, IRepositoryProvider repositoryProvider) : base(context, repositoryProvider) { }
    }

    public interface IMainUow : ICoreUnitOfWork { }
}
