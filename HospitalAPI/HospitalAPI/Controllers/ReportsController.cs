using HospitalAPI.Core.Dtos.ReportDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Helpers;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBranchRepository _branchRepository;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly UserManager<ApplicationUser> _user;

        public ReportsController(ApplicationDbContext context, IBranchRepository branchRepository, IHospitalRepository hospitalRepository, UserManager<ApplicationUser> user)
        {
            _context = context;
            _branchRepository = branchRepository;
            _hospitalRepository = hospitalRepository;
            _user = user;
        }
        // GET: api/<ReportssController>
        [HttpGet]
        public async Task<PatientCountAccordingToBranchBetweenTwoDatesWithTotals> GetPatientAccordingToBranchBetweenTwoDates(string startDate, string endDate)
        {
            var currentuser = await _user.FindByEmailFromClaimsPrinciple(HttpContext.User);
            // ReportLog.WriteTextFile("Patient Tally Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Request", "Ok");
            DateTime dateStartFrom = DateTime.Parse(startDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            var PatientCreatedBetweenTwoDate = await _context.Patient.Where(p => p.CreatedOn.Date >= dateStartFrom.Date && p.CreatedOn.Date <= dateEnd.Date)
                                                                         .ToListAsync();
            var getBrancWisePatientCount = PatientCreatedBetweenTwoDate.GroupBy(Bid => new { Bid.HospitalId , Bid.CreatedBy})
                                                                               .Select(x => new
                                                                               {
                                                                                   DoctordId = x.Key.CreatedBy,
                                                                                   BranchId = x.Key.HospitalId,
                                                                                   FemalePatient = x.Count(f => f.Gender == "Female"),
                                                                                   MalePatient = x.Count(f => f.Gender == "Male"),
                                                                                   TotalPatient = x.Count()
                                                                               })
                                                                               .OrderByDescending(x => x.DoctordId)
                                                                               .ToList();
            var totalMale = PatientCreatedBetweenTwoDate.Count(f => f.Gender == "Male");
            var totalFeMale = PatientCreatedBetweenTwoDate.Count(f => f.Gender == "Female");
            var totalPatient = PatientCreatedBetweenTwoDate.Count();

            List<PatientCountAccordingToBranchBetweenTwoDatesDto> result = new();
            foreach (var branch in getBrancWisePatientCount)
            {
                var branches =await _hospitalRepository.GetHospitalById((int)branch.BranchId);
                var user = await _user.FindByEmailAsync(branch.DoctordId);
                PatientCountAccordingToBranchBetweenTwoDatesDto branchWithCount = new()
                {
                    DoctorsName = user != null ? user.FirstName + " " + user.LastName : "Unknown",
                    BranchId = (int)branch.BranchId,
                    BranchName = branches.Name,
                    TotalMalePatient = branch.MalePatient,
                    TotalFemalePatient = branch.FemalePatient,
                    TotalPatient = branch.TotalPatient

                };
                result.Add(branchWithCount);
            }
            List<PatientCountAccordingToBranchBetweenTwoDatesDto> results = result.OrderBy(b => b.DoctorsName).ToList();

            PatientCountAccordingToBranchBetweenTwoDatesWithTotals patientCountAccordingToBranchBetweenTwoDatesWithTotals = new()
            {
                TotalMale = totalMale,
                TotalFeMale = totalFeMale,
                Total = totalPatient,
                PatientsCountAccordingToBranchBetweenTwoDatesDto = results
            };
            // ReportLog.WriteTextFile("Patient Tally Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Ok");
            return patientCountAccordingToBranchBetweenTwoDatesWithTotals;
        }

        [HttpGet("getprescriptionreport")]
        public async Task<PrescriptionCountAccordingToBranchBetweenTwoDatesDtoTotals> GetPrescriptionAccordingToBranchBetweenTwoDates(string startDate, string endDate)
        {
            var currentuser = await _user.FindByEmailFromClaimsPrinciple(HttpContext.User);
           // ReportLog.WriteTextFile("Prescription Tally Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Request", "Ok");
            DateTime dateStartFrom = DateTime.Parse(startDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            var PatientCreatedBetweenTwoDate = await _context.Prescription.Where(p => p.CreatedOn.Date >= dateStartFrom.Date && p.CreatedOn.Date <= dateEnd.Date)
                                                                          .Include(p=> p.Patient)
                                                                          .ToListAsync();
            var getBrancWisePatientCount = PatientCreatedBetweenTwoDate.GroupBy(Bid => new { Bid.HospitalId, Bid.DoctorId })
                                                                               .Select(x => new
                                                                               {
                                                                                   DoctordID = x.Key.DoctorId,
                                                                                   BranchId = x.Key.HospitalId,
                                                                                   FemalePatient = x.Count(f => f.Patient.Gender == "Female"),
                                                                                   MalePatient = x.Count(f => f.Patient.Gender == "Male"),
                                                                                   Telimedicine = x.Count(p => p.IsTelimedicine == true),
                                                                                   TotalPatient = x.Count()
                                                                               })
                                                                               .OrderByDescending(x => x.DoctordID)
                                                                               .ToList();
            var totalMale = PatientCreatedBetweenTwoDate.Count(f => f.Patient.Gender == "Male");
            var totalFeMale = PatientCreatedBetweenTwoDate.Count(f => f.Patient.Gender == "Female");
            var totalTelimedicien = PatientCreatedBetweenTwoDate.Count(f => f.IsTelimedicine == true);
            var totalPatient = PatientCreatedBetweenTwoDate.Count();


            List<PrescriptionCountAccordingToBranchBetweenTwoDatesDto> result = new();
            foreach (var branch in getBrancWisePatientCount)
            {
                var branches = await _hospitalRepository.GetHospitalById((int)branch.BranchId);
                var user =await _context.Users.FirstOrDefaultAsync(u => u.Id == branch.DoctordID);
                PrescriptionCountAccordingToBranchBetweenTwoDatesDto branchWithCount = new()
                {
                    DoctorsId = branch.DoctordID,
                    DoctorsName = user != null ? user.FirstName + " " + user.LastName : "Unknown",
                    BranchId = (int)branch.BranchId,
                    BranchName = branches.Name,
                    Telimedicine = branch.Telimedicine,
                    TotalMalePatient = branch.MalePatient,
                    TotalFemalePatient = branch.FemalePatient,
                    TotalPatient = branch.TotalPatient

                };
                result.Add(branchWithCount);
            }
            var results = result.OrderBy(b => b.DoctorsName).ToList();

            PrescriptionCountAccordingToBranchBetweenTwoDatesDtoTotals patientCountAccordingToBranchBetweenTwoDatesWithTotals = new()
            {
                TotalMale = totalMale,
                TotalFeMale = totalFeMale,
                Telimedicine = totalTelimedicien,
                Total = totalPatient,
                PatientsCountAccordingToBranchBetweenTwoDatesDto = results
            };
           // ReportLog.WriteTextFile("Prescription Tally Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Ok");
            return patientCountAccordingToBranchBetweenTwoDatesWithTotals;
        }

        // get prescription Telimedicine Report
        [HttpGet("gettelimedicinereport")]
        public async Task<PatientCountAccordingToBranchBetweenTwoDatesWithTotals> GetTelimedicineAccordingToBranchBetweenTwoDates(string startDate, string endDate)
        {
            var currentuser = await _user.FindByEmailFromClaimsPrinciple(HttpContext.User);
           // ReportLog.WriteTextFile("Telemedicine Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Request", "Ok");
            DateTime dateStartFrom = DateTime.Parse(startDate);
            DateTime dateEnd = DateTime.Parse(endDate);
            var telimedicinePrescription = _context.Prescription.Where(p => p.IsTelimedicine == true);
            var PatientCreatedBetweenTwoDate = await telimedicinePrescription.Where(p => p.CreatedOn.Date >= dateStartFrom.Date && p.CreatedOn.Date <= dateEnd.Date)
                                                                          .Include(p => p.Patient)
                                                                          .ToListAsync();
            var getBrancWisePatientCount = PatientCreatedBetweenTwoDate.GroupBy(Bid => new { Bid.HospitalId, Bid.DoctorId })
                                                                               .Select(x => new
                                                                               {
                                                                                   DoctordID = x.Key.DoctorId,
                                                                                   BranchId = x.Key.HospitalId,
                                                                                   FemalePatient = x.Count(f => f.Patient.Gender == "Female"),
                                                                                   MalePatient = x.Count(f => f.Patient.Gender == "Male"),
                                                                                   TotalPatient = x.Count()
                                                                               })
                                                                               .OrderByDescending(x => x.DoctordID)
                                                                               .ToList();
            var totalMale = PatientCreatedBetweenTwoDate.Count(f => f.Patient.Gender == "Male");
            var totalFeMale = PatientCreatedBetweenTwoDate.Count(f => f.Patient.Gender == "Female");
            var totalPatient = PatientCreatedBetweenTwoDate.Count();


            List<PatientCountAccordingToBranchBetweenTwoDatesDto> result = new();
            foreach (var branch in getBrancWisePatientCount)
            {
                var branches = await _hospitalRepository.GetHospitalById((int)branch.BranchId);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == branch.DoctordID);
                PatientCountAccordingToBranchBetweenTwoDatesDto branchWithCount = new()
                {
                    DoctorsId = branch.DoctordID,
                    DoctorsName = user != null ? user.FirstName + " " + user.LastName : "Unknown",
                    BranchId = (int)branch.BranchId,
                    BranchName = branches.Name,
                    TotalMalePatient = branch.MalePatient,
                    TotalFemalePatient = branch.FemalePatient,
                    TotalPatient = branch.TotalPatient

                };
                result.Add(branchWithCount);
            }
            var results = result.OrderBy(b => b.DoctorsName).ToList();

            PatientCountAccordingToBranchBetweenTwoDatesWithTotals patientCountAccordingToBranchBetweenTwoDatesWithTotals = new()
            {
                TotalMale = totalMale,
                TotalFeMale = totalFeMale,
                Total = totalPatient,
                PatientsCountAccordingToBranchBetweenTwoDatesDto = results
            };
           // ReportLog.WriteTextFile("Telemedicine Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Ok");
            return patientCountAccordingToBranchBetweenTwoDatesWithTotals;
        }

        // GET api/<ReportssController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportssController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportssController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportssController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
