using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivalManagement.Models.Models
{
    public class SecurityConfig
    {
        public string[] AllowedHosts { get; set; }

        public string[] AllowedIps { get; set; }
    }
}
