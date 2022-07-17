using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class PhysicalStateRepository : IPhysicalStateRepository
    {
        private readonly ApplicationDbContext _context;

        public PhysicalStateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<PhysicalState> PhysicalStateListAsync(string search)
        {
            IQueryable<PhysicalState> physicalStateList = _context.PhysicalState
                                    .Include(p => p.Patient)
                                    .Include(h => h.Hospital)
                                    .Include(v => v.VisitEntry);
            if (!String.IsNullOrEmpty(search))
            {
                physicalStateList = physicalStateList.Where(s => (s.Patient != null && s.Patient.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.Patient != null && s.Patient.LastName.ToLower().Contains(search.ToLower()))
                                          || (s.Hospital != null && s.Hospital.Name.ToLower().Contains(search.ToLower())));
            }
            return physicalStateList.OrderByDescending(o => o.Id);
        }
        public async Task<PhysicalState> GetPhysicalStateById(int id)
        {
            var physicalState = await _context.PhysicalState
                                    .Include(p => p.Patient)
                                    .Include(h => h.Hospital)
                                    .Include(v => v.VisitEntry)
                                    .FirstOrDefaultAsync(i => i.Id == id);
            return physicalState;
        }
    }
}
