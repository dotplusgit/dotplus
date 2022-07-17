using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.PatientDto;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Core.Models;
using HospitalAPI.Extensions;
using HospitalAPI.Errors;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Repository.IRepository;
using AutoMapper;
using HospitalAPI.Core.Dtos.VisitEntryDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Models.VisitEntryModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.Core.Calculator;
using HospitalAPI.Dto.PatientDto;
using HospitalAPI.Helpers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;

        public PatientController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 IPatientRepository PatientRepo,
                                 IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _patientRepo = PatientRepo;
            _mapper = mapper;
        }

        //GetSection
        [HttpGet]
        public async Task<ActionResult<PaginationsForPatient<PaginatedList<GetPatientfromApiDto>>>> GetPatient([FromQuery] Paramps paramps)
        {
            if(paramps.PageSize > 100)
            {
                paramps.PageSize = 100;
            }
            var queryablePatients = _patientRepo.GetPatientList(paramps.SearchString, paramps.Sort);
            var paginateddata =await PaginatedList<Patient>.CreateAsync(queryablePatients, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);

            var patients = paginateddata.Select(patient => new GetPatientfromApiDto
            {
                Address = patient.Address,
                BloodGroup = patient.BloodGroup,
                BranchId = patient.BranchId,
                BranchName = patient.Branch != null ? patient.Branch.Name : "EmptyBranch",
                Covidvaccine = patient.Covidvaccine,
                CreatedBy = patient.CreatedBy,
                CreatedOn = patient.CreatedOn,
                District = patient.District != null ? patient.District.Name : "EmptyDistrict",
                Division = patient.Division != null ? patient.Division.Name : "EmptyDivision",
                DoB = patient.DoB,
                FirstName = patient.FirstName,
                Gender = patient.Gender,
                HospitalName = patient.Hospital.Name,
                Id = patient.Id,
                IsActive = patient.IsActive,
                LastName = patient.LastName,
                MaritalStatus = patient.MaritalStatus,
                MembershipRegistrationNumber = patient.MembershipRegistrationNumber,
                MobileNumber = patient.MobileNumber,
                NID = patient.NID,
                Note = patient.Note,
                PrimaryMember = patient.PrimaryMember,
                Upazila = patient.Upazila != null ? patient.Upazila.Name : "EmptyUpazila",
                UpdatedBy = patient.UpdatedBy,
                UpdatedOn = patient.UpdatedOn,
                UserName = _patientRepo.UserNameByEmail(patient.CreatedBy),
                VaccineBrand = patient.VaccineBrand
            }).ToList();
            var result = new PaginationsForPatient<GetPatientfromApiDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await queryablePatients.CountAsync(), patients);
            return Ok(result);
            // return Ok(_mapper.Map<IReadOnlyList<Patient>, IReadOnlyList<GetPatientfromApiDto>>(patient));
            // return Ok(new Pagination<GetPatientfromApiDto>(pageNumber ?? 1, pageSize ?? 20, patient.Count, paginateddata));
            // return Ok(patients);
            //}
            //catch (Exception ex)
            //{
            //    return Ok(new GetPatientfromApiDto { FirstName = "Unknown", LastName = ex.Message }) ;
            //}
        }

        [HttpGet("patientWithVital")]
        public async Task<ActionResult<IReadOnlyList<GetPatientWithPhysicalStatDto>>> GetPatientWithVital()
        {
            var patientwithPhysicalStat = await _patientRepo.GetPatientWithLatestPhysicalStatList();
            return Ok(_mapper.Map<IReadOnlyList<Patient>, IReadOnlyList<GetPatientWithPhysicalStatDto>>(patientwithPhysicalStat));
        }

        [HttpGet("patientWithVital/{id}")]
        public async Task<ActionResult<GetPatientWithPhysicalStatDto>> GetPatientWithVital(int id)
        {
            var patient = await _patientRepo.GetPatientWithLatestVitalByPatientId(id);

            if (patient == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<GetPatientWithPhysicalStatDto>(patient));
        }

        [HttpGet("patienthistory/{patientId}")]
        public async Task<ActionResult<GetPatientHistory>> GetPatientHistory(int patientId)
        {

            var patient = await _patientRepo.GetPatientByPatientId(patientId);
            if (patient == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var getPatient = _mapper.Map<GetPatientWithPhysicalStatDto>(patient);
            var visitStatus = await _context.VisitEntry.Where(p => p.PatientId == patientId).OrderByDescending(d => d.Id).ToListAsync();
            var getvisitStatus = _mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(visitStatus);
            var prescription = await _context.Prescription.Where(p => p.PatientId == patientId)
                                                           .Include(p => p.MedicineForPrescription).ThenInclude(m => m.Medicine)
                                                           .OrderByDescending(d => d.Id).ToListAsync();
            var getPrescription = _mapper.Map<IReadOnlyList<Prescription>, IReadOnlyList<GetPrescriptionDto>>(prescription); ;
            var physicalState = await _context.PhysicalState.Where(p => p.PatientId == patientId).OrderByDescending(d => d.Id).ToListAsync();
            var getPhysicalState = _mapper.Map<IReadOnlyList<PhysicalState>, IReadOnlyList<GetPhysicalStateDto>>(physicalState); ;
            var patientHistory = new GetPatientHistory
            {
                Patient = getPatient,
                VisitEntries = (List<GetVisitEntryDto>)getvisitStatus,
                Prescription = (List<GetPrescriptionDto>)getPrescription,
                PhysicalState = (List<GetPhysicalStateDto>)getPhysicalState
            };

            return Ok(patientHistory);
        }
        [HttpGet("patientSearch")]
        public async Task<ActionResult<IReadOnlyList<GetPatientForSearch>>> GetPatientForSearch(string searchString)
        {
            var patient = await _patientRepo.GetPatientForSearchList(searchString);
            return Ok(_mapper.Map<IReadOnlyList<Patient>, IReadOnlyList<GetPatientForSearch>>(patient));
        }

        [HttpGet("patientSearchbyId")]
        public async Task<ActionResult<GetPatientForSearch>> GetPatientForSearchbyId(int id)
        {
            var patient = await _patientRepo.GetPatientByPatientIdForSearch(id);
            if(patient == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<GetPatientForSearch>(patient));
        }
        //PostSection
        [HttpPost]
        public async Task<ActionResult<ResponseObject>> PostPatientWithVitals(AddPatientWithPhysicalStatDto addPatientWithVital)
        {
            var existPatient =await _context.Patient
                                               .Where(fn => fn.FirstName == addPatientWithVital.FirstName)
                                               .Where(ln => ln.CreatedOn.Date == DateTime.Now.Date)
                                               .Where(ln => ln.MobileNumber == addPatientWithVital.MobileNumber)
                                               .Where(ln => ln.LastName == addPatientWithVital.LastName)
                                               .Where(ln => ln.FirstName == addPatientWithVital.FirstName)
                                               .Where(ln => ln.HospitalId == addPatientWithVital.HospitalId)
                                               .Where(ln => ln.Address == addPatientWithVital.Address)
                                               .Where(ln => ln.DistrictId == addPatientWithVital.DistrictId)
                                               .Where(ln => ln.UpazilaId == addPatientWithVital.UpazilaId)
                                               .ToListAsync();
            if (ModelState.IsValid)
            {
                try
                {
                    if(existPatient.Any())
                    {
                        return Ok(new ResponseObject { Message = "exist", IsValid = true });
                    }
                    var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

                    if(addPatientWithVital.DoB == null && (addPatientWithVital.Day != null || addPatientWithVital.Month != null || addPatientWithVital.Year != null))
                    {

                        var patientWithAge = new Patient
                        {
                            HospitalId = addPatientWithVital.HospitalId,
                            FirstName = addPatientWithVital.FirstName,
                            LastName = addPatientWithVital.LastName,
                            Age = addPatientWithVital.Age,
                            MobileNumber = addPatientWithVital.MobileNumber,
                            DoB = Calculate.DOBFromDayMonthYear(addPatientWithVital.Day, addPatientWithVital.Month, addPatientWithVital.Year),
                            Gender = addPatientWithVital.Gender,
                            MaritalStatus = addPatientWithVital.MaritalStatus,
                            PrimaryMember = addPatientWithVital.PrimaryMember,
                            MembershipRegistrationNumber = addPatientWithVital.MembershipRegistrationNumber,
                            Address = addPatientWithVital.Address,
                            DivisionId = addPatientWithVital.DivisionId,
                            UpazilaId = addPatientWithVital.UpazilaId,
                            DistrictId = addPatientWithVital.DistrictId,
                            NID = addPatientWithVital.NID,
                            BloodGroup = addPatientWithVital.BloodGroup,
                            BranchId = addPatientWithVital.BranchId,
                            IsActive = true,
                            Note = addPatientWithVital.Note,
                            Covidvaccine = addPatientWithVital.Covidvaccine,
                            VaccineBrand = addPatientWithVital.VaccineBrand,
                            VaccineDose = addPatientWithVital.VaccineDose,
                            FirstDoseDate = addPatientWithVital.FirstDoseDate,
                            SecondDoseDate = addPatientWithVital.SecondDoseDate,
                            BosterDoseDate = addPatientWithVital.BosterDoseDate,
                            CreatedBy = currentuser.Email,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = currentuser.Email,
                            UpdatedOn = DateTime.Now
                        };
                        _context.Patient.Add(patientWithAge);
                        await _context.SaveChangesAsync();

                        var patientVital1 = new PhysicalState
                        {
                            PatientId = patientWithAge.Id,
                            HospitalId = addPatientWithVital.HospitalId,
                            HeightFeet = addPatientWithVital.HeightFeet,
                            HeightInches = addPatientWithVital.HeightInches,
                            Anemia = addPatientWithVital.Anemia,
                            Jaundice = addPatientWithVital.Jaundice,
                            Dehydration = addPatientWithVital.Dehydration,
                            Abdomen = addPatientWithVital.Abdomen,
                            Appearance = addPatientWithVital.Appearance,
                            Cyanosis = addPatientWithVital.Cyanosis,
                            Edema = addPatientWithVital.Edema,
                            Heart = addPatientWithVital.Heart,
                            KUB = addPatientWithVital.KUB,
                            RbsFbs = addPatientWithVital.RbsFbs,
                            Lung = addPatientWithVital.Lung,
                            Weight = addPatientWithVital.Weight,
                            BMI = CalculateBMI(addPatientWithVital.HeightInches, addPatientWithVital.HeightFeet, addPatientWithVital.Weight),
                            Waist = addPatientWithVital.Waist,
                            Hip = addPatientWithVital.Hip,
                            SpO2 = addPatientWithVital.SpO2,
                            PulseRate = addPatientWithVital.PulseRate,
                            EditedBy = currentuser.Email,
                            EditedOn = DateTime.Now,
                            IsLatest = true,
                            BloodPressureSystolic = addPatientWithVital.BloodPressureSystolic,
                            BloodPressureDiastolic = addPatientWithVital.BloodPressureDiastolic,
                            BodyTemparature = addPatientWithVital.BodyTemparature,
                            CreatedBy = currentuser.Email,
                            CreatedOn = DateTime.Now,
                            HeartRate = addPatientWithVital.HeartRate,
                        };
                        _context.PhysicalState.Add(patientVital1);
                        await _context.SaveChangesAsync();
                        return Ok(new ResponseObject { Message = "success", IsValid = true, Data= patientWithAge.Id.ToString()});
                    }
                    else
                    {
                        var patient = new Patient
                        {
                            HospitalId = addPatientWithVital.HospitalId,
                            FirstName = addPatientWithVital.FirstName,
                            LastName = addPatientWithVital.LastName,
                            Age = addPatientWithVital.Age,
                            MobileNumber = addPatientWithVital.MobileNumber,
                            DoB = addPatientWithVital.DoB,
                            Gender = addPatientWithVital.Gender,
                            MaritalStatus = addPatientWithVital.MaritalStatus,
                            PrimaryMember = addPatientWithVital.PrimaryMember,
                            MembershipRegistrationNumber = addPatientWithVital.MembershipRegistrationNumber,
                            Address = addPatientWithVital.Address,
                            DivisionId = addPatientWithVital.DivisionId,
                            UpazilaId = addPatientWithVital.UpazilaId,
                            DistrictId = addPatientWithVital.DistrictId,
                            NID = addPatientWithVital.NID,
                            BloodGroup = addPatientWithVital.BloodGroup,
                            BranchId = addPatientWithVital.BranchId,
                            IsActive = true,
                            Note = addPatientWithVital.Note,
                            Covidvaccine = addPatientWithVital.Covidvaccine,
                            VaccineBrand = addPatientWithVital.VaccineBrand,
                            VaccineDose = addPatientWithVital.VaccineDose,
                            FirstDoseDate = addPatientWithVital.FirstDoseDate,
                            SecondDoseDate = addPatientWithVital.SecondDoseDate,
                            BosterDoseDate = addPatientWithVital.BosterDoseDate,
                            CreatedBy = currentuser.Email,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = currentuser.Email,
                            UpdatedOn = DateTime.Now
                        };
                        _context.Patient.Add(patient);
                        await _context.SaveChangesAsync();

                        var patientVital = new PhysicalState
                        {
                            PatientId = patient.Id,
                            HospitalId = addPatientWithVital.HospitalId,
                            HeightFeet = addPatientWithVital.HeightFeet,
                            HeightInches = addPatientWithVital.HeightInches,
                            Weight = addPatientWithVital.Weight,
                            BMI = CalculateBMI(addPatientWithVital.HeightInches, addPatientWithVital.HeightFeet, addPatientWithVital.Weight),
                            Anemia = addPatientWithVital.Anemia,
                            Jaundice = addPatientWithVital.Jaundice,
                            Dehydration = addPatientWithVital.Dehydration,
                            Abdomen = addPatientWithVital.Abdomen,
                            Appearance = addPatientWithVital.Appearance,
                            Cyanosis = addPatientWithVital.Cyanosis,
                            Edema = addPatientWithVital.Edema,
                            Heart = addPatientWithVital.Heart,
                            KUB = addPatientWithVital.KUB,
                            RbsFbs = addPatientWithVital.RbsFbs,
                            Lung = addPatientWithVital.Lung,
                            Waist = addPatientWithVital.Waist,
                            Hip = addPatientWithVital.Hip,
                            SpO2 = addPatientWithVital.SpO2,
                            PulseRate = addPatientWithVital.PulseRate,
                            EditedBy = currentuser.Email,
                            EditedOn = DateTime.Now,
                            IsLatest = true,
                            BloodPressureSystolic = addPatientWithVital.BloodPressureSystolic,
                            BloodPressureDiastolic = addPatientWithVital.BloodPressureDiastolic,
                            BodyTemparature = addPatientWithVital.BodyTemparature,
                            CreatedBy = currentuser.Email,
                            CreatedOn = DateTime.Now,
                            HeartRate = addPatientWithVital.HeartRate,
                        };
                        _context.PhysicalState.Add(patientVital);
                        await _context.SaveChangesAsync();
                        return Ok(new ResponseObject { Message = "success", IsValid = true , Data = patient.Id.ToString() });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new ResponseObject { Message = ex.Message, IsValid = false, Data = ex.InnerException }); ;
                }
            }
            return BadRequest(new ApiResponse(400));
        }


        [HttpPut]
        public async Task<IActionResult> PutPatient(UpdatePatientDto updatePatient)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            try
            {
                var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == updatePatient.Id);
                if (patient == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                patient.FirstName = updatePatient.FirstName;
                patient.LastName = updatePatient.LastName;
                patient.HospitalId = updatePatient.HospitalId;
                patient.BranchId = updatePatient.BranchId;
                patient.Address = updatePatient.Address;
                patient.DivisionId = updatePatient.DivisionId;
                patient.UpazilaId = updatePatient.UpazilaId;
                patient.DistrictId = updatePatient.DistrictId;
                patient.BloodGroup = updatePatient.BloodGroup;
                patient.DoB = updatePatient.DoB;
                patient.Gender = updatePatient.Gender;
                patient.IsActive = updatePatient.IsActive;
                patient.MaritalStatus = updatePatient.MaritalStatus;
                patient.MobileNumber = updatePatient.MobileNumber;
                patient.NID = updatePatient.NID;
                patient.Note = updatePatient.Note;
                patient.Covidvaccine = updatePatient.Covidvaccine;
                patient.VaccineBrand = updatePatient.VaccineBrand;
                patient.VaccineDose = updatePatient.VaccineDose;
                patient.FirstDoseDate = updatePatient.FirstDoseDate;
                patient.SecondDoseDate = updatePatient.SecondDoseDate;
                patient.BosterDoseDate = updatePatient.BosterDoseDate;
                patient.PrimaryMember = updatePatient.PrimaryMember;
                patient.MembershipRegistrationNumber = updatePatient.MembershipRegistrationNumber;
                patient.UpdatedBy = currentuser.Email;
                patient.UpdatedOn = DateTime.Now;
                _context.Patient.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseObject { Message = ex.Message, IsValid = false, Data=ex.InnerException });
            }
            return Ok(new ResponseObject { Message = "success", IsValid = true });
        }

        [HttpGet("bmi")]
        public double CalculateBMI(int? inch, int? feet, double? Weight)
        {

            var FeetToInch = 0;
            if (Weight == null || Weight < 1)
            {
                return 0;
            }
            if((feet == null && inch == null) || (feet < 1 && inch < 1))
            {
                return 0;
            }
            if (feet != null)
            {
                FeetToInch = (int)(feet * 12);
            }
            if(inch != null)
            {
               FeetToInch += (int)inch;
            }
            var HeightInMeter = FeetToInch * 0.0254;
            var BMI = Weight / (HeightInMeter * HeightInMeter);
            if (BMI != null)
            {
                return Math.Round(BMI.Value, 2);
            }
            else
            {
                return 0;
            }

        }
        [HttpGet("dob")]
        public DateTime Calculatedob(int? day, int? month, int? year)
        {

            return Calculate.DOBFromDayMonthYear(day, month, year);

        }
    }
}