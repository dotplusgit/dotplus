using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.VisitEntryDto
{
    public class AddVisitEntryCliant
    {
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int? HospitalId { get; set; }
        public int Serial { get; set; }
    }
}
