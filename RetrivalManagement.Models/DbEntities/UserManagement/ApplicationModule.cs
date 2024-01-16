using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.DbEntities.Main
{
    [Table("ApplicationModules", Schema = "dbo")]
    public partial class ApplicationModule
    {
        #region ApplicationModuleId Annotations

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        #endregion ApplicationModuleId Annotations

        public int ApplicationModuleId { get; set; }

        #region ModuleMasterId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion ModuleMasterId Annotations

        public int ModuleMasterId { get; set; }


        public Nullable<int> ParentApplicationModuleId { get; set; }

        #region ModuleMaster Annotations

        [ForeignKey(nameof(ModuleMasterId))]
        #endregion ModuleMaster Annotations

        public virtual ModuleMaster ModuleMaster { get; set; }

        #region RolePermissions Annotations

        [InverseProperty("ApplicationModule")]
        #endregion RolePermissions Annotations

        public virtual ICollection<RolePermission> RolePermissions { get; set; }


        public ApplicationModule()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}
