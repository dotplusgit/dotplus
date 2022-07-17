using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.MedicineDto
{
    public class AddMedicineDto
    {
        public string MedicineType { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public int ManufacturarId { get; set; }
    }
}
