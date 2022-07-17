using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict
{
    public class District
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        public int? DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }
    }
}
