using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RetrivalManagement.Models.DbEntities.ClientModule;
using RetrivalManagement.Models.DbEntities.Main;
using RxWeb.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models
{
    public class RetrivalManagementDbContext : DbContext, IRetrivalManagementDbContext
    {
        #region UserManagement
        public DbSet<ApplicationModule> ApplicationModules { get; set; }
        public DbSet<ModuleMaster> ModuleMasters { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Main
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DescriptionDetail> DescriptionDetails { get; set; }

        #endregion

        #region Client Module
        public DbSet<ClientRequest> ClientRequests { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        private static readonly string ConnectionString = "Server=retrivaldb.database.windows.net;Database=retrivaldb;User Id=sysadmin;Password=Retrival@123;TrustServerCertificate=True;MultipleActiveResultSets=true";
    }

    public interface IRetrivalManagementDbContext : IDbContext
    {
    }
}
