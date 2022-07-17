using HospitalAPI.Core.Models.FollowUpModel;
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
    public class FollowupRepository : IFollowupRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Followup> PendingFollowupRecordList(int hospitalId, string search)
        {
            IQueryable<Followup> pendingFollowup =  _context.Followup.Where(f => f.IsFollowup == false)
                                                         .Where(t => t.FollowupDate.Date <= DateTime.Now.Date)
                                                         .Where(h => h.HospitalId == hospitalId)
                                                         .Include(h => h.Hospital)
                                                         .Include(p => p.Prescription)
                                                         .Include(p => p.Patient)
                                                         .Include(u => u.ApplicationUser);
            if (!String.IsNullOrEmpty(search))
            {
                pendingFollowup = pendingFollowup.Where(s => (s.Patient != null && s.Patient.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.Patient != null && s.Patient.LastName.ToLower().Contains(search.ToLower()))
                                          || (s.Hospital != null && s.Hospital.Name.ToLower().Contains(search.ToLower())));
            }
            return pendingFollowup.OrderByDescending(f => f.FollowupDate.Date);
        }
        public async Task<IReadOnlyList<Followup>> IndividualPatientFollowUpRecordList(int patientId)
        {
            var patientFollowUpList = await _context.Followup.Where(p => p.PatientId == patientId)
                                                             .Include(h => h.Hospital)
                                                             .Include(p => p.Prescription)
                                                             .Include(p => p.Patient)
                                                             .Include(u => u.ApplicationUser)
                                                             .ToListAsync();
            return patientFollowUpList;
        }
        public async Task<IReadOnlyList<Followup>> IndividualPatientPendingFollowUpRecordList(int patientId)
        {
            var patientFollowUpList = await _context.Followup.Where(p => p.PatientId == patientId)
                                                             .Where(f => f.IsFollowup == false)
                                                             .Include(h => h.Hospital)
                                                             .Include(p => p.Prescription)
                                                             .Include(p => p.Patient)
                                                             .Include(u => u.ApplicationUser)
                                                             .ToListAsync();
            return patientFollowUpList;
        }
    }
}
