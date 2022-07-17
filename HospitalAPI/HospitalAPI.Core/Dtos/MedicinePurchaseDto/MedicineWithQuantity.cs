using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.MedicinePurchaseDto
{
    public class MedicineWithQuantity
    {
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
