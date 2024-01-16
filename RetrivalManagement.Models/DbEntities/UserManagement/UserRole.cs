using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.DbEntities.Main
{
    [Table("UserRoles", Schema = "dbo")]
    public partial class UserRole
    {
        #region UserRoleId Annotations

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        #endregion UserRoleId Annotations

        public int UserRoleId { get; set; }

        #region UserId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion UserId Annotations

        public int UserId { get; set; }

        #region RoleId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion RoleId Annotations

        public int RoleId { get; set; }

        #region Role Annotations

        [ForeignKey(nameof(RoleId))]
        #endregion Role Annotations

        public virtual Role Role { get; set; }

        #region User Annotations

        [ForeignKey(nameof(UserId))]
        #endregion User Annotations

        public virtual User User { get; set; }


        public UserRole()
        {
        }
    }
}
