using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict
{
    public class Upazila
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
