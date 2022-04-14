using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_WebApp.Core.Models
{
    public class RolesManagementViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string[] RoleNames { get; set; }
    }
}
