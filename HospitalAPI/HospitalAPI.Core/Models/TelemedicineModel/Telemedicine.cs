using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.TelemedicineModel
{
    public class Telemedicine
    {
        public Telemedicine()
        {

        }
        public Telemedicine(int? patietnId, string callerId, string receiverId, DateTime? callingTime)
        {
            PatietnId = patietnId;
            CallerId = callerId;
            ReceiverId = receiverId;
            CallingTime = callingTime;
        }

        public int Id { get; set; }
        public int? PatietnId { get; set; }
        public string CallerId { get; set; }
        [ForeignKey("CallerId")]
        public ApplicationUser Caller { get; set; }
        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
        public DateTime? CallingTime { get; set; }
    }
}
