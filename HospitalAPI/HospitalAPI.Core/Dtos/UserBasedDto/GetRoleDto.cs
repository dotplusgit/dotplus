using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos
{
    public class GetRoleDto
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
