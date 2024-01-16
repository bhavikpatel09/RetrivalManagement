using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.DbEntities.Main
{
    public partial class User
    {
        [NotMapped]
        public string OldPassword { get; set; }
        [NotMapped]
        public bool IsChangePassword { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        public string UserPassword { get; set; }

        [NotMapped]
        public string IPAddress { get; set; }

        [NotMapped]
        public bool IsEmailVerification { get; set; }

    }
}
