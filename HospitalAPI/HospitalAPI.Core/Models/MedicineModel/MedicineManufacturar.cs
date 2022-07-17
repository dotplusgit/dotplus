using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.MedicineModel
{
    public class MedicineManufacturar
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
