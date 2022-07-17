using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class PrescriptionWithMedicineAndDiagnosisDto
    {
        public AddPrescriptionFromAppDto PrescriptionDto { get; set; }
        public List<AddDiagnosisFromAppDto> DiagnosisList { get; set; }
        public List<AddMedicineForPrescriptionDto> MedicineForPrescription { get; set; }
    }
}
