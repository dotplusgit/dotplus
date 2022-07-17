using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using AutoMapper;
using HospitalAPI.Core.Dtos.BranchDto;
using HospitalAPI.Errors;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Extensions;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBranchRepository _branchRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BranchController(ApplicationDbContext context, 
                                IBranchRepository branchRepo,
                                IMapper mapper,
                                UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _branchRepo = branchRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Branch
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetBranchDto>>> GetBranch()
        {
            var branches =await _branchRepo.BranchListAsync();

            return Ok(_mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<GetBranchDto>> (branches));
        }

        [HttpGet("sortbyname")]
        public async Task<ActionResult<IReadOnlyList<GetBranchDtoSortByName>>> GetBranchSortByName()
        {
            var branches = await _branchRepo.BranchListSortByNameAsync();

            return Ok(_mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<GetBranchDtoSortByName>>(branches));
        }

        // GET: api/Branch/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBranchDto>> GetBranch(int id)
        {
            var branch = await _branchRepo.GetBranchById(id);

            if (branch == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<GetBranchDto>(branch));
        }

        // PUT: api/Branch/5
        [HttpPut("editbranch")]
        public async Task<IActionResult> PutBranch(EditBranchDto editBranchDto)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var branch =await _context.Branch.FirstOrDefaultAsync(b=> b.Id == editBranchDto.Id);
            if (branch == null)
            {
                return NotFound(new ApiResponse(404));
            }

            try
            {
                branch.BranchCode = editBranchDto.BranchCode;
                branch.Name = editBranchDto.Name;
                branch.Address = editBranchDto.Address;
                branch.DivisionId = editBranchDto.DivisionId;
                branch.DistrictId = editBranchDto.DistrictId;
                branch.UpazilaId = editBranchDto.UpazilaId;
                branch.IsActive = editBranchDto.IsActive;
                branch.UpdatedBy = currentuser.Email;
                branch.UpdatedOn = DateTime.Now;
                _context.Branch.Update(branch);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(new ResponseObject { Message = "success" });
        }

        // POST: api/Branch
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(AddBranchDto addBranchDto)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            if (ModelState.IsValid)
            {
                var branch = new Branch
                {
                    Name = addBranchDto.Name,
                    BranchCode = addBranchDto.BranchCode,
                    Address = addBranchDto.Address,
                    DivisionId = addBranchDto.DivisionId,
                    DistrictId = addBranchDto.DistrictId,
                    UpazilaId = addBranchDto.UpazilaId,
                    IsActive = addBranchDto.IsActive,
                    CreatedBy = currentuser.Email,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    UpdatedBy = currentuser.Email
                };
                _context.Branch.Add(branch);
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
