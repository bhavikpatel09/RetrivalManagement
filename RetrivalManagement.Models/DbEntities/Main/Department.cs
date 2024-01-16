using RetrivalManagement.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.DbEntities.Main
{
    [Table("Departments", Schema = "dbo")]
    public partial class Department
    {
        #region DepartmentId Annotations

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        #endregion DepartmentId Annotations

        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        #region StatusId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion StatusId Annotations

        public Status StatusId { get; set; }
        public Department()
        {
        }
    }
}
