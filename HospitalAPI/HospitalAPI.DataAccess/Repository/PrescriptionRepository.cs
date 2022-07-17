using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Prescription> PrescriptionListAsync(string sort, string search)
        {
            IQueryable<Prescription> prescriptions = _context.Prescription
                .Include(h => h.Hospital)
                .Include(p => p.Patient)
                .Include(v => v.VisitEntry);
            if (!String.IsNullOrEmpty(search))
            {
                prescriptions = prescriptions.Where(s => (s.DoctorFirstName != null && s.DoctorFirstName.ToLower().Contains(search.ToLower()))
                                          || (s.DoctorLastName != null && s.DoctorLastName.ToLower().Contains(search.ToLower()))
                                          || (s.Hospital != null && s.Hospital.Name.ToLower().Contains(search.ToLower()))
                                          || (s.Patient != null && s.Patient.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.Patient != null && s.Patient.LastName.ToLower().Contains(search.ToLower()))
                                          );
            }
            switch (sort)
            {
                case "hospitalNameAsc":
                    prescriptions = prescriptions.OrderBy(p => p.Hospital.Name);
                    break;
                case "hospitalNameDsc":
                    prescriptions = prescriptions.OrderByDescending(p => p.Hospital.Name);
                    break;
                case "doctorsNameAsc":
                    prescriptions = prescriptions.OrderBy(p => p.DoctorFirstName);
                    break;
                case "doctorsNameDsc":
                    prescriptions = prescriptions.OrderByDescending(p => p.DoctorFirstName);
                    break;
                case "patientNameAsc":
                    prescriptions = prescriptions.OrderBy(p => p.DoctorFirstName);
                    break;
                case "patientNameDsc":
                    prescriptions = prescriptions.OrderByDescending(p => p.DoctorFirstName);
                    break;
                default:
                    prescriptions = prescriptions.OrderByDescending(p => p.Id);
                    break;
            }
            // var prescriptionAsync = await prescriptions.ToListAsync();
            
            return prescriptions;
        }
        public async Task<Prescription> GetPriscriptionById(int id)
        {
            var prescription = await _context.Prescription
                            .Include(h => h.Hospital)
                            .Include(p => p.Patient)
                            .Include(p => p.PhysicalState)
                            //.Include(p => p.Patient.PhysicalStates.Where(i => i.IsLatest == true).Where(t => t.EditedOn.AddDays(3) >= DateTime.Now.Date))
                            .Include(v => v.VisitEntry)
                            .Include(p => p.Diagnosis).ThenInclude(d => d.DiseasesCategory)
                            .Include(p => p.Diagnosis).ThenInclude(d => d.Diseases)
                            .Include(p => p.MedicineForPrescription).ThenInclude(m => m.Medicine)
                            .FirstOrDefaultAsync(p => p.Id == id);
            return prescription;
        }

        public async Task<IReadOnlyList<Prescription>> GetPriscriptionForReportById(int id)
        {
            var prescription = await _context.Prescription
                            .Where(p => p.PatientId == id)
                            .Include(p => p.Hospital)
                            .Include(p => p.MedicineForPrescription).ThenInclude(m => m.Medicine)
                            .Include(p => p.Diagnosis).ThenInclude(d => d.DiseasesCategory)
                            .Include(p => p.Diagnosis).ThenInclude(d => d.Diseases)
                            .ToListAsync();
            return prescription;
        }
    }
}
