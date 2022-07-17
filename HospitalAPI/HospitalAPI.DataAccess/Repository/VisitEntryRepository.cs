using HospitalAPI.Core.Models.VisitEntryModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.DataAccess.StaticData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class VisitEntryRepository : IVisitEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IQueryable<VisitEntry> GetVisitEntryList(string search)
        {

            IQueryable<VisitEntry> visits = _context.VisitEntry
                       .Include(h => h.Hospital)
                       .Include(p => p.Patient)
                       .OrderByDescending(vE => vE.Date);
            if (!String.IsNullOrEmpty(search))
            {
                visits = visits.Where(s => (s.Patient != null && s.Patient.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.Patient != null && s.Patient.LastName.ToLower().Contains(search.ToLower()))
                                          || (s.Hospital != null && s.Hospital.Name.ToLower().Contains(search.ToLower())));
            }

            return visits;
        }
        public async Task<IReadOnlyList<VisitEntry>> GetTodaysActiveVisitEntryList()
        {
            var date = DateTime.Today.Date;
            var TodaysvisitEntry = await _context.VisitEntry
                           .Where(v => v.Date.Date == date)
                           .Where(v => v.Status == Status.Waiting)
                           .Include(h => h.Hospital)
                           .Include(p => p.Patient)
                           .OrderByDescending(v => v.Date)
                           .ToListAsync();

            return TodaysvisitEntry;
        }
        public async Task<VisitEntry> GetVisitEntryById(int id)
        {
            var visitEntry = await _context.VisitEntry
                .Include(h => h.Hospital)
                .Include(p => p.Patient)
                .OrderByDescending(v => v.Date)
                .FirstOrDefaultAsync(i => i.Id == id);
            return visitEntry;
        }

        public async Task<IReadOnlyList<VisitEntry>> GetVisitEntryByStatus(string status)
        {
            var visitEntry = await _context.VisitEntry
                        .Where(v => v.Status == status)
                        .Include(h => h.Hospital)
                        .Include(p => p.Patient)
                        .OrderByDescending(v => v.Date)
                        .ToListAsync();
            return visitEntry;
        }

    }
}
