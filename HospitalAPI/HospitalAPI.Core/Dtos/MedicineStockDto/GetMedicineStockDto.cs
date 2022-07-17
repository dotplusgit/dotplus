using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.MedicineDto
{
    public class GetMedicineStockDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string MedicineType { get; set; }
        public string Manufacturar { get; set; }
        public double Unit { get; set; }
        public double UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
