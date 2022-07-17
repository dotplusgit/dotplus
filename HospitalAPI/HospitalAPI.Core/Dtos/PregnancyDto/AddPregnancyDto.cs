using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PregnancyDto
{
    public class AddPregnancyDto
    {
        public int? PatientId { get; set; }
        public DateTime FirstDateOfLastPeriod { get; set; }
        public DateTime ExpectedDateOfDelivery { get; set; }
        public DateTime? NextCheckup { get; set; }
        public int? HospitalId { get; set; }
    }
}
