using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class AddDiagnosisFromAppDto
    {
        public string DiseaseName { get; set; }
        public int DiseasesId { get; set; }
        public int DiseasesCategoryId { get; set; }
    }
}
