using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.FollowUpModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.FollowUpDto;
using HospitalAPI.DataAccess.Repository.IRepository;
using AutoMapper;
using HospitalAPI.Helpers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFollowupRepository _followupRepository;
        private readonly IMapper _mapper;

        public FollowupController(ApplicationDbContext context, IFollowupRepository followupRepository, IMapper mapper)
        {
            _context = context;
            _followupRepository = followupRepository;
            _mapper = mapper;
        }

        // GET: api/followup/getpendingfollowup
        [HttpGet("getpendingfollowup/{hospitalId}")]
        public async Task<ActionResult<Pagination<PaginatedList<GetFollowUpListDto>>>> GetPendingFollowup([FromQuery] Paramps paramps,int hospitalId)
        {
            var pendingFollowup = _followupRepository.PendingFollowupRecordList(hospitalId, paramps.SearchString);
            var paginateddata = await PaginatedList<Followup>.CreateAsync(pendingFollowup, paramps.PageNumber ?? 1, paramps.PageSize ?? 50);
            var mappedData = _mapper.Map<PaginatedList<Followup>, PaginatedList<GetFollowUpListDto>>(paginateddata);
            return Ok(new Pagination<GetFollowUpListDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20,await pendingFollowup.CountAsync(), mappedData));
        }


        // DELETE: api/Followup/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFollowup(int id)
        //{
        //    var followup = await _context.Followup.FindAsync(id);
        //    if (followup == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Followup.Remove(followup);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
