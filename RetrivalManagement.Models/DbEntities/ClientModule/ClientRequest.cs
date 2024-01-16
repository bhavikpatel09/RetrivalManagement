using RetrivalManagement.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetrivalManagement.Models.DbEntities.Main;

namespace RetrivalManagement.Models.DbEntities.ClientModule
{
    [Table("ClientRequests", Schema = "dbo")]
    public partial class ClientRequest
    {
        #region ClientRequestId Annotations

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        #endregion ClientRequestId Annotations

        public int ClientRequestId { get; set; }

        #region ClientId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion ClientId Annotations

        public int ClientId { get; set; }

        #region Client Annotations

        [ForeignKey(nameof(ClientId))]
        #endregion Client Annotations

        public virtual Client Client { get; set; }

        #region DepartmentId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion DepartmentId Annotations

        public int DepartmentId { get; set; }

        #region Department Annotations

        [ForeignKey(nameof(DepartmentId))]
        #endregion Department Annotations

        public virtual Department Department { get; set; }

        #region CategoryId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion CategoryId Annotations

        public int CategoryId { get; set; }

        #region Category Annotations

        [ForeignKey(nameof(CategoryId))]
        #endregion Category Annotations

        public virtual Category Category { get; set; }
        public ClientRequest()
        {
        }
    }
}
