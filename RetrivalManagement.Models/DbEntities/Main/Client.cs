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
    [Table("Clients", Schema = "dbo")]
    public partial class Client
    {
        #region ClientId Annotations

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Key]
        #endregion ClientId Annotations

        public int ClientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        #region StatusId Annotations

        [Range(1, int.MaxValue)]
        [Required]
        #endregion StatusId Annotations

        public Status StatusId { get; set; }
        public Client()
        {
        }
    }
}
