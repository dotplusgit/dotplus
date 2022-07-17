using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Core.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using HospitalAPI.Extensions;
using HospitalAPI.Errors;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.Core.Models.FollowUpModel;
using HospitalAPI.Helpers;
using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using HospitalAPI.DataAccess.StaticData;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPrescriptionRepository _prescriptionRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IFollowupRepository _followupRepository;

        public PrescriptionController(ApplicationDbContext context, 
                                       IPrescriptionRepository prescriptionRepo,
                                       UserManager<ApplicationUser> userManager,
                                       IMapper mapper,
                                       IFollowupRepository followupRepository)
        {
            _context = context;
            _prescriptionRepo = prescriptionRepo;
            _userManager = userManager;
            _mapper = mapper;
            _followupRepository = followupRepository;
        }

        // GET: api/Prescription
        [HttpGet]
        public async Task<ActionResult<Pagination<PaginatedList<GetPrescriptionDto>>>> GetPrescription([FromQuery] Paramps paramps)
        {
            if (paramps.PageSize > 100)
            {
                paramps.PageSize = 100;
            }
            var prescriptions =  _prescriptionRepo.PrescriptionListAsync(paramps.Sort, paramps.SearchString);
            var paginateddata = await PaginatedList<Prescription>.CreateAsync(prescriptions, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
           
            var mappedData =  _mapper.Map<PaginatedList<Prescription>, PaginatedList<GetPrescriptionDto>>(paginateddata);

            return Ok(new Pagination<GetPrescriptionDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20,await prescriptions.CountAsync(), mappedData));
        }

        // GET: api/Prescription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPrescriptionDto>> GetPrescription(int id)
        {
            var prescription = await _prescriptionRepo.GetPriscriptionById(id);

            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetPrescriptionDto>(prescription));
        }

        // PUT: api/Prescription/5
        [HttpPut]
        public async Task<IActionResult> PutPrescription( UpdatePrescriptionDto updatePrescription)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var prescription = await _prescriptionRepo.GetPriscriptionById(updatePrescription.Id);
            var previousFollowUp = await _followupRepository.IndividualPatientPendingFollowUpRecordList(prescription.PatientId);
            if (prescription.MedicineForPrescription.Any())
            {
                ICollection<MedicineForPrescription> medicineForPrescriptions = prescription.MedicineForPrescription.ToList();
                foreach (MedicineForPrescription medicineForPrescription in medicineForPrescriptions)
                {
                    _context.MedicineForPrescription.Remove(medicineForPrescription);
                    await  _context.SaveChangesAsync();
                }
                if (updatePrescription.MedicineForPrescription.Any())
                {
                    foreach (var medicine in updatePrescription.MedicineForPrescription)
                    {
                        MedicineForPrescription newMedicineForPrescription = new()
                        {
                            PrescriptionId = prescription.Id,
                            MedicineId = medicine.MedicineId,
                            Dose = medicine.Dose,
                            Comment = medicine.Comment,
                            Time = medicine.Time,
                            UpdatedOn = DateTime.Now

                        };
                        _context.MedicineForPrescription.Add(newMedicineForPrescription);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                if (updatePrescription.MedicineForPrescription.Any())
                {
                    foreach (var medicine in updatePrescription.MedicineForPrescription)
                    {
                        MedicineForPrescription newMedicineForPrescription = new()
                        {
                            PrescriptionId = prescription.Id,
                            MedicineId = medicine.MedicineId,
                            Dose = medicine.Dose,
                            Comment = medicine.Comment,
                            Time = medicine.Time,
                            UpdatedOn = DateTime.Now

                        };
                        _context.MedicineForPrescription.Add(newMedicineForPrescription);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            if(prescription.Diagnosis.Any())
            {
                ICollection<Diagnosis> diagnosis =  prescription.Diagnosis.ToList();
                foreach(Diagnosis dignosis in diagnosis)
                {
                    _context.Diagnosis.Remove(dignosis);
                    await _context.SaveChangesAsync();
                }

               //  var diagnoses = new List<Diagnosis>();
               if(updatePrescription.Diagnosis.Any())
                {
                    foreach (var dignosis in updatePrescription.Diagnosis)
                    {
                        Diagnosis diagnosis1 = new()
                        {
                            PrescriptionId = prescription.Id,
                            PatientId = prescription.PatientId,
                            DiseasesCategoryId = dignosis.DiseasesCategoryId,
                            DiseasesId = dignosis.DiseasesId,
                            UpdatedBy = currentuser.Email,
                            UpdatedAt = DateTime.Now
                        };
                        _context.Diagnosis.Add(diagnosis1);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                if (updatePrescription.Diagnosis.Any())
                {
                    foreach (var dignosis in updatePrescription.Diagnosis)
                    {
                        Diagnosis diagnosis1 = new()
                        {
                            PrescriptionId = prescription.Id,
                            PatientId = prescription.PatientId,
                            DiseasesCategoryId = dignosis.DiseasesCategoryId,
                            DiseasesId = dignosis.DiseasesId,
                            UpdatedBy = currentuser.Email,
                            UpdatedAt = DateTime.Now
                        };
                        _context.Diagnosis.Add(diagnosis1);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            if(previousFollowUp.Any())
            {
                foreach(var folowup in previousFollowUp)
                {
                    folowup.IsFollowup = true;
                    _context.Followup.Update(folowup);
                    await _context.SaveChangesAsync();
                }
            }
            if (prescription == null)
            {
                return NotFound(new ApiResponse(404));
            }
            try
            {
                prescription.AdviceMedication = updatePrescription.AdviceMedication;
                prescription.AdviceTest = updatePrescription.AdviceTest;
                prescription.DoctorsObservation = updatePrescription.DoctorsObservation;
                prescription.MH = updatePrescription.MH;
                prescription.OH = updatePrescription.OH;
                prescription.DX = updatePrescription.DX;
                // prescription.HospitalId = updatePrescription.HospitalId;
                prescription.NextVisit = updatePrescription.NextVisit;
                prescription.Note = updatePrescription.Note;
                prescription.AllergicHistory = updatePrescription.AllergicHistory;
                prescription.FamilyHistory = updatePrescription.FamilyHistory;
                prescription.HistoryOfPastIllness = updatePrescription.HistoryOfPastIllness;
                prescription.SystemicExamination = updatePrescription.SystemicExamination;
                prescription.IsTelimedicine = updatePrescription.IsTelimedicine;
               // prescription.PatientId = updatePrescription.PatientId;
                prescription.UpdatedOn = DateTime.Now;
                // prescription.VisitEntryId = updatePrescription.VisitEntryId;
                _context.Prescription.Update(prescription);
                await _context.SaveChangesAsync();
                if(prescription.NextVisit !=  null)
                {
                    var followUp = new Followup(prescription.PatientId, currentuser.Id, prescription.Id, prescription.HospitalId, (DateTime)prescription.NextVisit, false);
                    _context.Followup.Add(followUp);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseObject { Message = ex.Message }) ;
            }
            return Ok(new ResponseObject { Message = "Success", IsValid = true});
        }

        // POST: api/Prescription
        [HttpPost]
        public async Task<ActionResult<Prescription>> PostPrescription(AddPrescriptionDto addPrescription)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    AdviceMedication = addPrescription.AdviceMedication,
                    AdviceTest = addPrescription.AdviceTest,
                    CreatedOn = DateTime.Now,
                    DoctorFirstName = currentuser.FirstName,
                    DoctorLastName = currentuser.LastName,
                    BMDCRegNo = currentuser.BMDCRegNo,
                    OptionalEmail = currentuser.OptionalEmail,
                    DoctorId = currentuser.Id,
                    DoctorsObservation = addPrescription.DoctorsObservation,
                    HospitalId = addPrescription.HospitalId,
                    Note = addPrescription.Note,
                    PatientId = addPrescription.PatientId,
                    // PhysicalStateId = addPrescription.PhysicalStateId,
                    UpdatedOn = DateTime.Now,
                    VisitEntryId = addPrescription.VisitEntryId,
                    
                };
                _context.Prescription.Add(prescription);
                await _context.SaveChangesAsync();
                return Ok(prescription);
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }

        // POST: api/Prescription
        [HttpPost("postprescriptioncliant")]
        public async Task<ActionResult<Prescription>> PostPrescriptionCliant(AddPrescriptionDtoCliant addPrescription)
        {

            if(addPrescription.HospitalId != null)
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                // var latestvisitEntry = _context.VisitEntry.Where(v => v.Date.Date == DateTime.Today.Date);
                var CurrentVisit = await _context.VisitEntry.FirstOrDefaultAsync(v => v.Id == addPrescription.VisitEntryId);
                var existPrescription = _context.Prescription.Where(p => p.PatientId == CurrentVisit.PatientId)
                                                             .Where(p => p.DoctorId == currentuser.Id)
                                                             .Where(p => p.CreatedOn.Date == DateTime.Now.Date)
                                                             .Any();
                var recentPhysicalStat =await _context.PhysicalState.Where(p => p.PatientId == CurrentVisit.PatientId)
                                                                    .Where(t => t.EditedOn.AddDays(3) >= DateTime.Now.Date && t.IsLatest == true)
                                                                    .OrderByDescending(p => p.EditedOn)
                                                                    .FirstOrDefaultAsync();

                if (existPrescription)
                {
                    return Ok(new ResponseObject { Message = "exist", IsValid = true });
                }

                if (ModelState.IsValid)
                {
                    var prescription = new Prescription
                    {
                        DoctorFirstName = currentuser.FirstName,
                        DoctorLastName = currentuser.LastName,
                        BMDCRegNo = currentuser.BMDCRegNo,
                        OptionalEmail = currentuser.OptionalEmail,
                        DoctorId = currentuser.Id,
                        DoctorsObservation = addPrescription.DoctorsObservation,
                        HospitalId = (int)addPrescription.HospitalId,
                        VisitEntryId = CurrentVisit.Id,
                        PatientId = CurrentVisit.PatientId,
                        AdviceMedication = addPrescription.AdviceMedication,
                        AdviceTest = addPrescription.AdviceTest,
                        AllergicHistory = addPrescription.AllergicHistory,
                        FamilyHistory = addPrescription.FamilyHistory,
                        HistoryOfPastIllness = addPrescription.HistoryOfPastIllness,
                        MH = addPrescription.MH,
                        OH = addPrescription.OH,
                        DX = addPrescription.DX,
                        SystemicExamination = addPrescription.SystemicExamination,
                        Note = addPrescription.Note,
                        IsTelimedicine = false,
                        PhysicalStateId = recentPhysicalStat?.Id,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    };
                    _context.Prescription.Add(prescription);
                    await _context.SaveChangesAsync();
                    return Ok(prescription);
                }
                else
                {
                    return BadRequest(new ApiResponse(400));
                }
            }
            else
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                // var latestvisitEntry = _context.VisitEntry.Where(v => v.Date.Date == DateTime.Today.Date);
                var CurrentVisit = await _context.VisitEntry.FirstOrDefaultAsync(v => v.Id == addPrescription.VisitEntryId);
                var recentPhysicalStat = await _context.PhysicalState.Where(p => p.PatientId == CurrentVisit.PatientId)
                                                    .Where(t => t.EditedOn.AddDays(3) >= DateTime.Now.Date && t.IsLatest == true)
                                                    .OrderByDescending(p => p.EditedOn)
                                                    .FirstOrDefaultAsync();
                var existPrescription = _context.Prescription.Where(p => p.PatientId == CurrentVisit.PatientId)
                                             .Where(p => p.DoctorId == currentuser.Id)
                                             .Where(p => p.CreatedOn.Date == DateTime.Now.Date)
                                             .Any();
                if (existPrescription)
                {
                    return Ok(new ResponseObject { Message = "exist", IsValid = true });
                }

                if (ModelState.IsValid)
                {
                    var prescription = new Prescription
                    {
                        DoctorFirstName = currentuser.FirstName,
                        DoctorLastName = currentuser.LastName,
                        BMDCRegNo = currentuser.BMDCRegNo,
                        OptionalEmail = currentuser.OptionalEmail,
                        DoctorId = currentuser.Id,
                        DoctorsObservation = addPrescription.DoctorsObservation,
                        HospitalId = currentuser.HospitalId,
                        VisitEntryId = CurrentVisit.Id,
                        PatientId = CurrentVisit.PatientId,
                        AdviceMedication = addPrescription.AdviceMedication,
                        AdviceTest = addPrescription.AdviceTest,
                        AllergicHistory = addPrescription.AllergicHistory,
                        FamilyHistory = addPrescription.FamilyHistory,
                        HistoryOfPastIllness = addPrescription.HistoryOfPastIllness,
                        MH = addPrescription.MH,
                        OH = addPrescription.OH,
                        DX = addPrescription.DX,
                        SystemicExamination = addPrescription.DX,
                        Note = addPrescription.Note,
                        IsTelimedicine = false,
                        PhysicalStateId = recentPhysicalStat?.Id,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                    };
                    _context.Prescription.Add(prescription);
                    await _context.SaveChangesAsync();
                    return Ok(prescription);
                }
                else
                {
                    return BadRequest(new ApiResponse(400));
                }
            }
            
        }


        // POST: api/Prescription
        [HttpPost("postprescriptionapp")]
        public async Task<ActionResult<Prescription>> PostPrescriptionApp(PrescriptionWithMedicineAndDiagnosisDto prescriptionWithMedicineAndDiagnosis)
        {
            var patient = await _context.Patient.FindAsync(prescriptionWithMedicineAndDiagnosis.PrescriptionDto.PatientId);

            if(patient == null)
            {
                return NotFound(new ResponseObject{Message ="patientnotfound", IsValid = false, Data = "Patient Not Found With This Id" });
            }
           try
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var recentPhysicalStat = await _context.PhysicalState.Where(p => p.PatientId == prescriptionWithMedicineAndDiagnosis.PrescriptionDto.PatientId)
                                                        .Where(t => t.EditedOn.AddDays(3) >= DateTime.Now.Date && t.IsLatest == true)
                                                        .OrderByDescending(p => p.EditedOn)
                                                        .FirstOrDefaultAsync();
                var previousFollowUp = await _followupRepository.IndividualPatientPendingFollowUpRecordList(prescriptionWithMedicineAndDiagnosis.PrescriptionDto.PatientId);
                
                // Update Followup
                if (previousFollowUp.Any())
                {
                    foreach (var folowup in previousFollowUp)
                    {
                        folowup.IsFollowup = true;
                        _context.Followup.Update(folowup);
                    }
                    await _context.SaveChangesAsync();
                }



                if (ModelState.IsValid)
                {
                    // add Visit Entry
                    VisitEntry visit = new()
                    {
                        PatientId = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.PatientId,
                        HospitalId = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.HospitalId,
                        Status = Status.Done,
                        Date = DateTime.Now,
                        Serial = 0
                    };
                    _context.VisitEntry.Add(visit);
                    await _context.SaveChangesAsync();

                    // Create Prescription
                    Prescription newPrescription = new()
                    {
                        PatientId = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.PatientId,
                        HospitalId = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.HospitalId,
                        DoctorsObservation = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.DoctorsObservation,
                        AdviceTest = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.AdviceTest,
                        OH = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.OH,
                        SystemicExamination = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.SystemicExamination,
                        HistoryOfPastIllness = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.HistoryOfPastIllness,
                        FamilyHistory = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.FamilyHistory,
                        AllergicHistory = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.AllergicHistory,
                        NextVisit = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.NextVisit,
                        IsTelimedicine = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.IsTelimedicine,
                        IsAfternoon = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.IsAfternoon,
                        Note = prescriptionWithMedicineAndDiagnosis.PrescriptionDto.Note,
                        DoctorId = currentuser.Id,
                        DoctorFirstName = currentuser.FirstName,
                        DoctorLastName = currentuser.LastName,
                        BMDCRegNo = currentuser.BMDCRegNo,
                        PhysicalStateId = recentPhysicalStat?.Id,
                        VisitEntryId = visit.Id,
                        UpdatedOn = DateTime.Now,
                        CreatedOn = DateTime.Now,
                        

                    };

                    var prescriptionAddResult = _context.Prescription.Add(newPrescription);
                    await _context.SaveChangesAsync();

                    // Add Next Visit
                    if (newPrescription.NextVisit != null)
                    {
                        var followUp = new Followup(newPrescription.PatientId, currentuser.Id, newPrescription.Id, newPrescription.HospitalId, (DateTime)newPrescription.NextVisit, false);
                        _context.Followup.Add(followUp);
                        await _context.SaveChangesAsync();
                    }

                    // Add Medicine

                    if (prescriptionWithMedicineAndDiagnosis.MedicineForPrescription.Any())
                    {
                        foreach (var medicine in prescriptionWithMedicineAndDiagnosis.MedicineForPrescription)
                        {
                            MedicineForPrescription medicineForPrescription = new()
                            {
                                PrescriptionId = newPrescription.Id,
                                MedicineId = medicine.MedicineId,
                                Dose = medicine.Dose,
                                Comment = medicine.Comment,
                                Time = medicine.Time,
                                UpdatedOn = DateTime.Now
                            };

                            _context.MedicineForPrescription.Add(medicineForPrescription);
                        }
                        await _context.SaveChangesAsync();
                    }

                    // Add Diagnosis
                    if (prescriptionWithMedicineAndDiagnosis.DiagnosisList.Any())
                    {
                        foreach (var dignosis in prescriptionWithMedicineAndDiagnosis.DiagnosisList)
                        {
                            Diagnosis newdiagnosis = new()
                            {
                                PrescriptionId = newPrescription.Id,
                                PatientId = newPrescription.PatientId,
                                DiseasesCategoryId = dignosis.DiseasesCategoryId,
                                DiseasesId = dignosis.DiseasesId,
                                UpdatedBy = currentuser.Email,
                                UpdatedAt = DateTime.Now
                            };
                            _context.Diagnosis.Add(newdiagnosis);
                        }
                        await _context.SaveChangesAsync();
                    }

                }

              return Ok(new ResponseObject { Message = "success", IsValid = true });
                
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseObject { Message = "failed", IsValid = false, Data=ex.Message });
            }
        }

    }
}