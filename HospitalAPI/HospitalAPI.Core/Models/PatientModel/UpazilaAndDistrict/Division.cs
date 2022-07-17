using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict
{
    public class Division
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
