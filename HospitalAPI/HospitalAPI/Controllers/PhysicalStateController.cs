using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Core.Models;
using HospitalAPI.Extensions;
using HospitalAPI.Errors;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.Core.Calculator;
using HospitalAPI.Helpers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicalStateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhysicalStateRepository _physicalStateRepo;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhysicalStateController(ApplicationDbContext context, 
                                       IPhysicalStateRepository physicalStateRepo,
                                       IMapper mapper,
                                       IPatientRepository PatientRepo,
                                       UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _physicalStateRepo = physicalStateRepo;
            _mapper = mapper;
            _patientRepo = PatientRepo;
            _userManager = userManager;
        }

        // GET: api/PhysicalState
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<PaginatedList<GetPhysicalStateDto>>>>> GetPhysicalState([FromQuery] Paramps paramps)
        {
            var physicalStates = _physicalStateRepo.PhysicalStateListAsync(paramps.SearchString);

            var paginateddata = await PaginatedList<PhysicalState>.CreateAsync(physicalStates, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
            var mappingData = _mapper.Map<PaginatedList<PhysicalState>, PaginatedList<GetPhysicalStateDto>>(paginateddata);
            return Ok(new Pagination<GetPhysicalStateDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await physicalStates.CountAsync(), mappingData));
        }

        // GET: api/PhysicalState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPhysicalStateDto>> GetPhysicalState(int id)
        {
            var physicalState = await _physicalStateRepo.GetPhysicalStateById(id);

            if (physicalState == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetPhysicalStateDto>(physicalState));
        }

        // PUT: api/PhysicalState/5
        [HttpPut]
        public async Task<IActionResult> PutPhysicalState(EditPhysicalStateDto editPhysicalState)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var physicalState = await _context.PhysicalState.FirstOrDefaultAsync(b => b.Id == editPhysicalState.Id);
            if (physicalState == null)
            {
                return NotFound(new ApiResponse(404));
            }

            try
            {
                physicalState.HospitalId = currentuser.HospitalId;
                physicalState.PatientId = editPhysicalState.PatientId;
                physicalState.VisitEntryId = editPhysicalState.VisitEntryId;
                physicalState.BodyTemparature = editPhysicalState.BodyTemparature;
                physicalState.Anemia = editPhysicalState.Anemia;
                physicalState.Jaundice = editPhysicalState.Jaundice;
                physicalState.Dehydration = editPhysicalState.Dehydration;
                physicalState.Abdomen = editPhysicalState.Abdomen;
                physicalState.Appearance = editPhysicalState.Appearance;
                physicalState.Cyanosis = editPhysicalState.Cyanosis;
                physicalState.Edema = editPhysicalState.Edema;
                physicalState.Heart = editPhysicalState.Heart;
                physicalState.KUB = editPhysicalState.KUB;
                physicalState.RbsFbs = editPhysicalState.RbsFbs;
                physicalState.Lung = editPhysicalState.Lung;
                physicalState.HeightFeet = editPhysicalState.HeightFeet;
                physicalState.HeightInches = editPhysicalState.HeightInches;
                physicalState.BloodPressureSystolic = editPhysicalState.BloodPressureSystolic;
                physicalState.BloodPressureDiastolic = editPhysicalState.BloodPressureDiastolic;
                physicalState.HeartRate = editPhysicalState.HeartRate;
                physicalState.PulseRate = editPhysicalState.PulseRate;
                physicalState.SpO2 = editPhysicalState.SpO2;
                physicalState.Weight = editPhysicalState.Weight;
                physicalState.BMI = Calculate.BMI(physicalState.HeightFeet, physicalState.HeightInches, physicalState.Weight);
                physicalState.EditedBy = currentuser.Email;
                physicalState.EditedOn = DateTime.Now;
                _context.PhysicalState.Update(physicalState);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(new ResponseObject { Message = "success" });
        }

        // POST: api/PhysicalState
        [HttpPost]
        public async Task<ActionResult> PostPhysicalState(AddPhysicalStateDto addPhysicalState)
        {
            var patient = await _context.Patient.FindAsync(addPhysicalState.PatientId);
            if (patient == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var physicalstate = await _context.PhysicalState.Where(p => p.PatientId == addPhysicalState.PatientId).ToListAsync();
            if (physicalstate != null)
            {
                foreach (var PStat in physicalstate)
                {
                    PStat.IsLatest = false;
                    _context.PhysicalState.Update(PStat);
                }
                await _context.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                var physicalState = new PhysicalState
                {
                    BloodPressureSystolic = addPhysicalState.BloodPressureSystolic,
                    BloodPressureDiastolic = addPhysicalState.BloodPressureDiastolic,
                    BodyTemparature = addPhysicalState.BodyTemparature,
                    Anemia = addPhysicalState.Anemia,
                    Jaundice = addPhysicalState.Jaundice,
                    Dehydration = addPhysicalState.Dehydration,
                    Abdomen = addPhysicalState.Abdomen,
                    Appearance = addPhysicalState.Appearance,
                    Cyanosis = addPhysicalState.Cyanosis,
                    Edema = addPhysicalState.Edema,
                    Heart = addPhysicalState.Heart,
                    KUB = addPhysicalState.KUB,
                    RbsFbs = addPhysicalState.RbsFbs,
                    Lung = addPhysicalState.Lung,
                    HeartRate = addPhysicalState.HeartRate,
                    HospitalId = currentuser.HospitalId,
                    PatientId = addPhysicalState.PatientId,
                    VisitEntryId = addPhysicalState.VisitEntryId,
                    Weight = addPhysicalState.Weight,
                    HeightFeet = addPhysicalState.HeightFeet,
                    HeightInches = addPhysicalState.HeightInches,
                    BMI = Calculate.BMI(addPhysicalState.HeightFeet, addPhysicalState.HeightInches, addPhysicalState.Weight),
                    IsLatest = true,
                    PulseRate = addPhysicalState.PulseRate,
                    SpO2 = addPhysicalState.SpO2,
                    Hip = addPhysicalState.Hip,
                    Waist = addPhysicalState.Waist,
                    CreatedBy = currentuser.Email,
                    CreatedOn = DateTime.Now,
                    EditedBy = currentuser.Email,
                    EditedOn = DateTime.Now,
                };
                _context.PhysicalState.Add(physicalState);
                await _context.SaveChangesAsync();
                return Ok(new ResponseObject { Message = "success" });
            }
            else
            {
                return BadRequest(new ApiResponse(400));
            }
        }
    }
}
