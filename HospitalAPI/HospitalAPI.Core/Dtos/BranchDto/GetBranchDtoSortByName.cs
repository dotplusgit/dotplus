using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.BranchDto
{
    public class GetBranchDtoSortByName
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Name { get; set; }
    }
}
