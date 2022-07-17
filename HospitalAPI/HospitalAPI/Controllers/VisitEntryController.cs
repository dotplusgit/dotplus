using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.VisitEntryModel;
using HospitalAPI.DataAccess.Data;
using AutoMapper;
using HospitalAPI.Core.Dtos.VisitEntryDto;
using HospitalAPI.Errors;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.StaticData;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Core.Models;
using HospitalAPI.Extensions;
using HospitalAPI.Helpers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitEntryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IVisitEntryRepository _visitEntryRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;

        public VisitEntryController(ApplicationDbContext context,
                                    IVisitEntryRepository visitEntryRepo,
                                    IPatientRepository PatientRepo,
                                    UserManager<ApplicationUser> userManager,
                                    IMapper mapper)
        {
            _context = context;
            _visitEntryRepo = visitEntryRepo;
            _userManager = userManager;
            _patientRepo = PatientRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<PaginatedList<GetVisitEntryDto>>>>> GetVisitEntry([FromQuery] Paramps paramps)
        {
            var visits = _visitEntryRepo.GetVisitEntryList(paramps.SearchString);

           //  return Ok(_mapper.Map<IReadOnlyList<VisitEntry> ,IReadOnlyList<GetVisitEntryDto>>(visits));
            var paginateddata = await PaginatedList<VisitEntry>.CreateAsync(visits, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
            var mappedData = _mapper.Map<PaginatedList<VisitEntry>, PaginatedList<GetVisitEntryDto>>(paginateddata);

            return Ok(new Pagination<GetVisitEntryDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20,await visits.CountAsync(), mappedData));
        }
        // Cliant Get Hospitalwise AllVisit
        [HttpGet("getvisitentricliant")]
        public async Task<ActionResult<IReadOnlyList<Pagination<PaginatedList<GetVisitEntryDto>>>>> GetVisitEntryCliant([FromQuery] Paramps paramps, int? hospitalId)
        {
            if(hospitalId == null)
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var visits =  _visitEntryRepo.GetVisitEntryList(paramps.SearchString);
                var cliantVisit = visits.Where(v => v.HospitalId == currentuser.HospitalId);
                // return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(cliantVisit));
                var paginateddata = await PaginatedList<VisitEntry>.CreateAsync(cliantVisit, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
                var mappedData = _mapper.Map<PaginatedList<VisitEntry>, PaginatedList<GetVisitEntryDto>>(paginateddata);

                return Ok(new Pagination<GetVisitEntryDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await cliantVisit.CountAsync(), mappedData));
            }
            else
            {
                // var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var visits =  _visitEntryRepo.GetVisitEntryList(paramps.SearchString);
                var cliantVisit = visits.Where(v => v.HospitalId == hospitalId);
                var paginateddata = await PaginatedList<VisitEntry>.CreateAsync(cliantVisit, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
                var mappedData = _mapper.Map<PaginatedList<VisitEntry>, PaginatedList<GetVisitEntryDto>>(paginateddata);

                return Ok(new Pagination<GetVisitEntryDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await cliantVisit.CountAsync(), mappedData));
                //  return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(cliantVisit));
            }
        }


        [HttpGet("todaysVisit")]
        public async Task<ActionResult<GetVisitEntryDto>> GetCurrentVisitEntry()
        {
            var visitEntry = await _visitEntryRepo.GetTodaysActiveVisitEntryList();

            if (visitEntry == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(visitEntry));
        }

        [HttpGet("todaysVisitcliant")]
        public async Task<ActionResult<GetVisitEntryDto>> GetCurrentVisitEntryCliant(int? hospitalId)
        {
            if(hospitalId == null)
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var visitEntry = await _visitEntryRepo.GetTodaysActiveVisitEntryList();
                var visitCliant = visitEntry.Where(v => v.HospitalId == currentuser.HospitalId).ToList();

                if (visitCliant == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(visitCliant));
            }
            else
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var visitEntry = await _visitEntryRepo.GetTodaysActiveVisitEntryList();
                var visitCliant = visitEntry.Where(v => v.HospitalId == hospitalId).ToList();

                if (visitCliant == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(visitCliant));
            }

        }

        // GET: api/VisitEntry/5
        [HttpGet("byid/{id}")]
        public async Task<ActionResult<GetVisitEntryDto>> GetVisitEntry(int id)
        {
            var visitEntry = await _visitEntryRepo.GetVisitEntryById(id);

            if (visitEntry == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetVisitEntryDto>(visitEntry));
        }

        //Visit Entries Serial ************----------******
        [HttpGet("latestserial")]
        public async Task<ActionResult<int>> GetLastVisitSerialSpecificDate()
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var latestvisitEntry =await _context.VisitEntry.Where(h => h.HospitalId == currentuser.HospitalId)
                                                           .Where(v => v.Date.Date == DateTime.Today.Date)
                                                           .ToListAsync();
            if(latestvisitEntry.Count == 0)
            {
                return 1;
            }
            else
            {
                var vis = latestvisitEntry.Max(m => m.Serial);
                return vis + 1;

            }
        }
        [HttpGet("latestserial/{date}")]
        public async Task<ActionResult<int>> GetLastVisitSerialSpecificDate(string date)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            DateTime enteredDate = DateTime.Parse(date);

            var latestvisitEntry = await _context.VisitEntry.Where(h => h.HospitalId == currentuser.HospitalId)
                                                                         .Where(v => v.Date.Date == enteredDate.Date)
                                                                         .ToListAsync();
            if (latestvisitEntry.Count == 0)
            {
                return 1;
            }
            else
            {
                var vis = latestvisitEntry.Max(m => m.Serial);
                return vis + 1;

            }
        }
        [HttpGet("status/{status}")]
        public async Task<ActionResult<GetVisitEntryDto>> GetVisitEntry(string status)
        {
            var visitEntry =await _visitEntryRepo.GetVisitEntryByStatus(status);

            if (visitEntry == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<IReadOnlyList<VisitEntry>, IReadOnlyList<GetVisitEntryDto>>(visitEntry));
        }

        // get Serial According to Hospital and Date.
        [HttpGet("lastserialbydateandhospital")]
        public async Task<ActionResult<int>> GetLastVisitSerialSpecificDateAndHospital(string date, int? hospitalId)
        {
            if (hospitalId != null)
            {
                // var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                DateTime enteredDate = DateTime.Parse(date);

                var latestvisitEntry = await _context.VisitEntry.Where(h => h.HospitalId == hospitalId)
                                                                             .Where(v => v.Date.Date == enteredDate.Date)
                                                                             .ToListAsync();
                if (latestvisitEntry.Count == 0)
                {
                    return 1;
                }
                else
                {
                    var vis = latestvisitEntry.Max(m => m.Serial);
                    return vis + 1;

                }
            }
            else
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                DateTime enteredDate = DateTime.Parse(date);

                var latestvisitEntry = await _context.VisitEntry.Where(h => h.HospitalId == currentuser.HospitalId)
                                                                             .Where(v => v.Date.Date == enteredDate.Date)
                                                                             .ToListAsync();
                if (latestvisitEntry.Count == 0)
                {
                    return 1;
                }
                else
                {
                    var vis = latestvisitEntry.Max(m => m.Serial);
                    return vis + 1;

                }
            }
            
        }


        // Http Put Section--------***---------
        [HttpPut("putvisitentry")]
        public async Task<IActionResult> PutVisitEntry(UpdateVisitEntryDto updateVisitEntry)
        {
            var visitEntry =await _context.VisitEntry.FirstOrDefaultAsync(v => v.Id == updateVisitEntry.Id);
            if(visitEntry == null)
            {
                return NotFound(new ApiResponse(404));
            }

           if(ModelState.IsValid)
            {
                visitEntry.PatientId = updateVisitEntry.PatientId;
                visitEntry.HospitalId = updateVisitEntry.HospitalId;
                visitEntry.Serial = updateVisitEntry.Serial;
                visitEntry.Date = updateVisitEntry.Date;
                visitEntry.Status = updateVisitEntry.Status;
            }
            _context.Entry(visitEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new ResponseObject{ Message = "success" });
        }
        [HttpPut("putvisitentrycliant")]
        public async Task<IActionResult> PutVisitEntryCliant(UpdateVisitEntryDto updateVisitEntry)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var visitEntry = await _context.VisitEntry.FirstOrDefaultAsync(v => v.Id == updateVisitEntry.Id);
            if (visitEntry == null)
            {
                return NotFound(new ApiResponse(404));
            }

            if (ModelState.IsValid)
            {
                visitEntry.PatientId = updateVisitEntry.PatientId;
                visitEntry.Serial = updateVisitEntry.Serial;
                visitEntry.Date = updateVisitEntry.Date;
                visitEntry.Status = updateVisitEntry.Status;
            }
            _context.Entry(visitEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new ResponseObject { Message = "success" });
        }

        [HttpPut("putvisitentrystatus")]
        public async Task<IActionResult> PutVisitEntryStatus(UpdateVisitEntryStatusDto updateVisitStatusEntry)
        {
            var visitEntry = await _context.VisitEntry.FirstOrDefaultAsync(v => v.Id == updateVisitStatusEntry.Id);
            if (visitEntry == null)
            {
                return NotFound(new ApiResponse(404));
            }

            if (ModelState.IsValid)
            {
                visitEntry.Status = updateVisitStatusEntry.Status;
            }
            _context.Entry(visitEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new ResponseObject { Message = "success" });
        }


        // Http Post Section ******---------******

        [HttpPost]
        public async Task<ActionResult<ResponseObject>> PostVisitEntry(AddVisitEntryDto addVisitEntry)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400));
            }
            var visitEntry = new VisitEntry
            {
                HospitalId = addVisitEntry.HospitalId,
                PatientId = addVisitEntry.PatientId,
                Date = addVisitEntry.Date,
                Serial = addVisitEntry.Serial,
                Status = Status.Waiting
               };
               _context.VisitEntry.Add(visitEntry);
            await _context.SaveChangesAsync();

            return Ok(new ResponseObject {Message = "success", IsValid = true });
        }

        [HttpPost("postvisitentrybyuser")]
        public async Task<ActionResult<ResponseObject>> PostVisitEntryByUser(AddVisitEntryCliant addVisitEntry)
        {
            var patient = await _patientRepo.GetPatientByPatientIdForSearch(addVisitEntry.PatientId);
            if (patient == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var existingVisit =  _context.VisitEntry.Where(v=> v.HospitalId == addVisitEntry.HospitalId)
                                                    .Where(v => v.PatientId == addVisitEntry.PatientId)
                                                    .Where(v => v.Date.Date == addVisitEntry.Date.Date)
                                                    .Any();
            if (existingVisit)
            {
                return Ok(new ResponseObject { Message = "exist", IsValid = false });
            }
            if (addVisitEntry.HospitalId != null)
            {
                // var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse(400));
                }
                var visitEntry = new VisitEntry
                {
                    HospitalId = (int)addVisitEntry.HospitalId,
                    PatientId = addVisitEntry.PatientId,
                    Date = addVisitEntry.Date,
                    Serial = addVisitEntry.Serial,
                    Status = Status.Waiting
                };
                _context.VisitEntry.Add(visitEntry);
                await _context.SaveChangesAsync();

                return Ok(new ResponseObject { Message = "success", IsValid = true });
            }
            else
            {
                var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse(400));
                }
                var visitEntry = new VisitEntry
                {
                    HospitalId = currentuser.HospitalId,
                    PatientId = addVisitEntry.PatientId,
                    Date = addVisitEntry.Date,
                    Serial = addVisitEntry.Serial,
                    Status = Status.Waiting
                };
                _context.VisitEntry.Add(visitEntry);
                await _context.SaveChangesAsync();

                return Ok(new ResponseObject { Message = "success", IsValid = true });
            }
            
        }
    }
}
