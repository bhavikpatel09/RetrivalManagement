using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.Models
{
    public class ChangePasswordViewModel
    {
        public string EmailAddress { get; set; }
        public string OldPassword { get; set; }
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserId { get; set; }
        public bool IsChangePassword { get; set; }
    }
}
