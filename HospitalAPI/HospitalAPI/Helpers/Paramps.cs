using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Helpers
{
    public class Paramps
    {
        public string SearchString { get; set; }
        public string Sort { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
