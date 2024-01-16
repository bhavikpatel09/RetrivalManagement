using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.Constants
{
    public class RegexConstant
    {
        public const string systemVariablePattern = @"\{{.*?\}}";

        public const string systemEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const string systemPasswordPattern = @"^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[-.!@#$%^&*()_=+/\\'])([a-zA-Z0-9-.!@#$%^&*()_=+/\\']+)$";
    }
}
