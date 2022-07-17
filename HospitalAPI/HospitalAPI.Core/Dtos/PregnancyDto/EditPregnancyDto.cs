using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PregnancyDto
{
    public class EditPregnancyDto
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? HospitalId { get; set; }
        public DateTime FirstDateOfLastPeriod { get; set; }
        public DateTime ExpectedDateOfDelivery { get; set; }
    }
}
