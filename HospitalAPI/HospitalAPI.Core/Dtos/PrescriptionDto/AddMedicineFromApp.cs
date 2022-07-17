using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class AddMedicineFromApp
    {
        public int MedicineId { get; set; }
        public string MedicineType { get; set; }
        public string BrandName { get; set; }
        public string Dose { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
    }
}
