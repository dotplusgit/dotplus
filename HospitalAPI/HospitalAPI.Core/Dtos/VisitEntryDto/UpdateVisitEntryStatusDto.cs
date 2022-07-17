using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.VisitEntryDto
{
    public class UpdateVisitEntryStatusDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
