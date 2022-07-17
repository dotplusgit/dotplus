using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.BranchDto
{
    public class AddBranchDto
    {
        public string BranchCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? DivisionId { get; set; }
        public int? UpazilaId { get; set; }
        public int? DistrictId { get; set; }
        public bool IsActive { get; set; }
    }
}
