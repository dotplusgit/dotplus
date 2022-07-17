using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.TelemedicineDto
{
    public class GetTelemedicineDto
    {
        public int Id { get; set; }
        public int? PatietnId { get; set; }
        public string CallerId { get; set; }
        public string CallerFirstName { get; set; }
        public string CallerLastName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public DateTime? CallingTime { get; set; }
    }
}
