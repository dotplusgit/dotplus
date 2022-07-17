using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.TelemedicineDto
{
    public class AddTelemedicineDto
    {
        public int? PatietnId { get; set; }
        public string CallerId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime? CallingTime { get; set; }
    }
}
