using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.MedicineDto
{
    public class UpdateMedicineStockDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string MedicineType { get; set; }
        public string Manufacturar { get; set; }
        public double Unit { get; set; }
        public double UnitPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
