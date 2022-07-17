using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.MedicinePurchaseDto
{
    public class MedicinePurchaseDto
    {
        public int? PrescriptionId { get; set; }
        public IReadOnlyList<MedicineWithQuantity> PurchaseMedicineList { get; set; }
    }
}
