﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.HospitalDto
{
    public class HospitalAddDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int? BranchId { get; set; }
        public int? DivisionId { get; set; }
        public int? UpazilaId { get; set; }
        public int? DistrictId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
