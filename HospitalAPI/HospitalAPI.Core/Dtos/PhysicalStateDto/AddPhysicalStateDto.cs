using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PhysicalStateDto
{
    public class AddPhysicalStateDto
    {
        public int PatientId { get; set; }
        public int? VisitEntryId { get; set; }
        public string BloodPressureSystolic { get; set; }
        public string BloodPressureDiastolic { get; set; }
        public string HeartRate { get; set; }
        public string BodyTemparature { get; set; }
        public string Appearance { get; set; }
        public string Anemia { get; set; }
        public string Jaundice { get; set; }
        public string Dehydration { get; set; }
        public string Edema { get; set; }
        public string Cyanosis { get; set; }
        public string Heart { get; set; }
        public string Lung { get; set; }
        public string Abdomen { get; set; }
        public string KUB { get; set; }
        public string RbsFbs { get; set; }
        public int? HeightFeet { get; set; }
        public int? HeightInches { get; set; }
        public double? Weight { get; set; }
        public string Waist { get; set; }
        public string Hip { get; set; }
        public double? SpO2 { get; set; }
        public double? PulseRate { get; set; }
        public bool? IsLatest { get; set; }
    }
}
