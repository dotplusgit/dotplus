using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PregnancyDto
{
    public class AddMonthlyCheckup
    {
        public int Id { get; set; }
        public int PregnancyId { get; set; }
        public DateTime CheckupDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
