using AutoMapper;
using HospitalAPI.Core.Dtos.PatientDto;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using HospitalAPI.Core.Dtos.ReportDto;
using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Extensions;
using HospitalAPI.Helpers;
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
    public class MedicalReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPrescriptionRepository _prescriptionRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalReportController(ApplicationDbContext context, 
                                       IPrescriptionRepository prescriptionRepo,
                                        UserManager<ApplicationUser> userManager,
                                       IMapper mapper)
        {
            _context = context;
            _prescriptionRepo = prescriptionRepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: api/<MedicalReportController>
        [HttpGet]
        public async Task<MedicalReportDto> GetMedicalReport(int patientId)
        {
            try
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                // ReportLog.WriteTextFile("Medical Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Request", "Ok");
                var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == patientId);
                var patientPrescription = await _prescriptionRepo.GetPriscriptionForReportById(patientId);
                var patientVitals = await _context.PhysicalState.Where(p => p.PatientId == patientId).ToListAsync();
                var mappedPatient = _mapper.Map<GetPatientDto>(patient);
                List<VitalAndPrescriptionDto> vitalAndPrescription = new List<VitalAndPrescriptionDto>();
                foreach (var prescription in patientPrescription)
                {
                    var mappedPrescription = _mapper.Map<GetPrescriptionDto>(prescription);
                    var physicalStat = patientVitals.Where(p => p.CreatedOn.Date == prescription.CreatedOn.Date).FirstOrDefault();
                    VitalAndPrescriptionDto vitalAndPrescriptionDto = new();
                    if (physicalStat == null)
                    {
                        GetPhysicalStateDto getPhysicalState = new GetPhysicalStateDto();
                        vitalAndPrescriptionDto.Prescription = mappedPrescription;
                        vitalAndPrescriptionDto.Vital = getPhysicalState;
                        vitalAndPrescription.Add(vitalAndPrescriptionDto);

                    }
                    else
                    {
                        var mappedPhysicalStat = _mapper.Map<GetPhysicalStateDto>(physicalStat);
                        vitalAndPrescriptionDto.Prescription = mappedPrescription;
                        vitalAndPrescriptionDto.Vital = mappedPhysicalStat;
                        vitalAndPrescription.Add(vitalAndPrescriptionDto);
                    }
                }
                MedicalReportDto medicalReportDto = new()
                {
                    Patient = mappedPatient,
                    PatientVitalAndPrescription = vitalAndPrescription

                };
                // ReportLog.WriteTextFile("Medical Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Ok");
                return medicalReportDto;
            }
            catch(Exception ex)
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                MedicalReportDto medicalReportDto = new();
                // ReportLog.WriteTextFile("Medical Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Failed", ex.Message);
                return medicalReportDto;
            }
        }

        // GET api/<MedicalReportController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicalReportController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicalReportController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalReportController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
