using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.DignosisDto
{
    public class AddDagnosisDto
    {

        public int? DiseasesCategoryId { get; set; }
        public int DiseasesId { get; set; }
        public string DiseasesName { get; set; }

    }
}
