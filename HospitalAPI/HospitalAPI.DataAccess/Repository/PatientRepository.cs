using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext context;

        public PatientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Patient> GetPatientList(string search, string sort)
        {
            IQueryable<Patient> patients =  context.Patient.Where(p => p.IsActive)
                            .Include(h => h.Hospital)
                            .Include(b => b.Branch)
                            .Include(b => b.Upazila)
                            .Include(b => b.District)
                            .Include(b => b.Division);
            if (!String.IsNullOrEmpty(search))
            {
                patients = patients.Where(s => (s.FirstName != null && s.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.LastName != null && s.LastName.ToLower().Contains(search.ToLower()))
                                          || (s.Branch != null && s.Branch.Name.ToLower().Contains(search.ToLower()))
                                          || (s.Hospital != null && s.Hospital.Name.ToLower().Contains(search.ToLower()))
                                          || (s.MobileNumber != null && s.MobileNumber.ToLower().Contains(search.ToLower())));
            }
            switch (sort)
            {
                case "nameAsc":
                    patients = patients.OrderBy(p => p.FirstName);
                    break;
                case "nameDsc":
                    patients =   patients.OrderByDescending(p => p.FirstName);
                    break;
                case "dobAsc":
                    patients = patients.OrderBy(p => p.DoB);
                    break;
                case "dobDsc":
                    patients = patients.OrderByDescending(p => p.DoB);
                    break;
                case "createdOnAsc":
                    patients = patients.OrderBy(p => p.CreatedOn);
                    break;
                case "createdOnDsc":
                    patients = patients.OrderByDescending(p => p.CreatedOn);
                    break;
                default:
                    patients = patients.OrderByDescending(p => p.Id);
                    break;
            }
            // var patientbysort =await patients.ToListAsync();

            return patients;
        }
        public async Task<IReadOnlyList<Patient>> GetPatientForSearchList(string search)
        {
            var patients = context.Patient.Where(p => p.IsActive).OrderByDescending(i => i.Id);
            var patientbysort = await patients.ToListAsync();
            if (!String.IsNullOrEmpty(search))
            {
                patientbysort = patientbysort.Where(s => (s.FirstName != null && s.FirstName.ToLower().Contains(search.ToLower()))
                                          || (s.LastName != null && s.LastName.ToLower().Contains(search.ToLower()))
                                          || (s.MobileNumber != null && s.MobileNumber.ToLower().Contains(search.ToLower()))).Take(15).ToList();
            }
            return patientbysort;
        }
        public async Task<Patient> GetPatientByPatientId(int id)
        {
            var patient = await context.Patient
                            .Include(h => h.Hospital)
                            .Include(b => b.Branch)
                            .Include(b => b.Division)
                            .Include(b => b.Upazila)
                            .Include(b => b.District)
                            .Include(v => v.PhysicalStates.OrderByDescending(i => i.Id))
                            .FirstOrDefaultAsync(p => p.Id == id);
            return patient;
        }
        public async Task<IReadOnlyList<Patient>> GetPatientWithLatestPhysicalStatList()
        {
            var patients = await context.Patient
                           .Include(h => h.Hospital)
                           .Include(b => b.Branch)
                           .Include(b => b.Division)
                           .Include(b => b.District)
                           .Include(b => b.Upazila)
                           .Include(v => v.PhysicalStates.Where(l => l.IsLatest == true))
                           .ToListAsync();
            return patients;
        }

        public async Task<Patient> GetPatientWithLatestVitalByPatientId(int id)
        {
            var patient = await context.Patient
                           .Include(h => h.Hospital)
                           .Include(b => b.Branch)
                           .Include(b => b.Division)
                           .Include(b => b.District)
                           .Include(b => b.Upazila)
                           .Include(v => v.PhysicalStates.Where(l => l.IsLatest == true))
                           .FirstOrDefaultAsync(p => p.Id == id);
            return patient;
        }

        public string UserNameByEmail(string email)
        {
            var user = context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user.Result.FirstName + " " + user.Result.LastName;
            }
            else
            {
                return "Unknown";
            }
        }

        public async Task<Patient> GetPatientByPatientIdForSearch(int id)
        {
            var patient = await context.Patient
                            .FirstOrDefaultAsync(p => p.Id == id);
            return patient;
        }
    }
}
