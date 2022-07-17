using HospitalAPI.Core.Dtos.ReportDto;
using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Dto;
using HospitalAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomePageReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: api/<HomePageReportController>
        [HttpGet("currentmonthpatientreport")]
        public async Task<HomePageReportDto> GetCurrentMonthPatientReport()
        {
            var currentMonth = DateTime.Now;
            var firstDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            var totalDaysOfCurrentMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            var lastDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, totalDaysOfCurrentMonth);
            var allData = _context.Patient;
            var totalData = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
            string monthName = currentMonth.ToString("MMMM");

            List<WeeklyPatientCountDto> weeklyPatientCounts = new();

            List<DateTime> allFridayDate = new List<DateTime>();

            for (int i = 1; i <= totalDaysOfCurrentMonth; i++)
            {
                DateTime loopDay = new DateTime(currentMonth.Year, currentMonth.Month, i);
                if (loopDay.DayOfWeek == DayOfWeek.Friday)
                {
                    allFridayDate.Add(loopDay);
                }
                else
                {
                    continue;
                }
            }

            for(int j = 0; j <= allFridayDate.Count; j++)
            {
                if(j == 0)
                {
                    var totalPatietnDateRange =await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                } else if( j == allFridayDate.Count)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j-1].AddDays(1) && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
                    DateTime dateTime = allFridayDate[j-1];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, lastDateOfCurrentMonth.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                } else
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j-1].AddDays(1) && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
            }

            HomePageReportDto homePageReport = new HomePageReportDto(monthName, totalData, weeklyPatientCounts);

            return homePageReport;
        }

        // GET api/<HomePageReportController>/5
        [HttpGet("previousmonthpatientreport")]
        public async Task<HomePageReportDto> GetpreviousMonthPatientReport()
        {
            var currentMonth = DateTime.Now.AddMonths(-1);
            var firstDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            var totalDaysOfCurrentMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            var lastDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, totalDaysOfCurrentMonth);
            var allData =  _context.Patient;
            var totalData = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
            string monthName = currentMonth.ToString("MMMM");

            List<WeeklyPatientCountDto> weeklyPatientCounts = new();

            List<DateTime> allFridayDate = new List<DateTime>();

            for (int i = 1; i <= totalDaysOfCurrentMonth; i++)
            {
                DateTime loopDay = new DateTime(currentMonth.Year, currentMonth.Month, i);
                if (loopDay.DayOfWeek == DayOfWeek.Friday)
                {
                    allFridayDate.Add(loopDay);
                }
                else
                {
                    continue;
                }
            }

            for (int j = 0; j <= allFridayDate.Count; j++)
            {
                if (j == 0)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else if (j == allFridayDate.Count)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
                    DateTime dateTime = allFridayDate[j - 1];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, lastDateOfCurrentMonth.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
            }

            HomePageReportDto homePageReport = new HomePageReportDto(monthName, totalData, weeklyPatientCounts);

            return homePageReport;
        }


        // GET: api/<HomePageReportController>
        [HttpGet("currentmonthprescriptionreport")]
        public async Task<HomePageReportDto> GetCurrentMonthPrescriptionReport()
        {
            var currentMonth = DateTime.Now;
            var firstDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            var totalDaysOfCurrentMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            var lastDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, totalDaysOfCurrentMonth);
            var allData = _context.Prescription;
            var totalData = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
            string monthName = currentMonth.ToString("MMMM");

            List<WeeklyPatientCountDto> weeklyPatientCounts = new();

            List<DateTime> allFridayDate = new List<DateTime>();

            for (int i = 1; i <= totalDaysOfCurrentMonth; i++)
            {
                DateTime loopDay = new DateTime(currentMonth.Year, currentMonth.Month, i);
                if (loopDay.DayOfWeek == DayOfWeek.Friday)
                {
                    allFridayDate.Add(loopDay);
                }
                else
                {
                    continue;
                }
            }

            for (int j = 0; j <= allFridayDate.Count; j++)
            {
                if (j == 0)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else if (j == allFridayDate.Count)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
                    DateTime dateTime = allFridayDate[j - 1];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, lastDateOfCurrentMonth.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
            }

            HomePageReportDto homePageReport = new HomePageReportDto(monthName, totalData, weeklyPatientCounts);

            return homePageReport;
        }

        // GET api/<HomePageReportController>/5
        [HttpGet("previousmonthprescriptionreport")]
        public async Task<HomePageReportDto> GetpreviousMonthPrescriptionReport()
        {
            var currentMonth = DateTime.Now.AddMonths(-1);
            var firstDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
            var totalDaysOfCurrentMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
            var lastDateOfCurrentMonth = new DateTime(currentMonth.Year, currentMonth.Month, totalDaysOfCurrentMonth);
            var allData = _context.Prescription;
            var totalData = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
            string monthName = currentMonth.ToString("MMMM");

            List<WeeklyPatientCountDto> weeklyPatientCounts = new();

            List<DateTime> allFridayDate = new List<DateTime>();

            for (int i = 1; i <= totalDaysOfCurrentMonth; i++)
            {
                DateTime loopDay = new DateTime(currentMonth.Year, currentMonth.Month, i);
                if (loopDay.DayOfWeek == DayOfWeek.Friday)
                {
                    allFridayDate.Add(loopDay);
                }
                else
                {
                    continue;
                }
            }

            for (int j = 0; j <= allFridayDate.Count; j++)
            {
                if (j == 0)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= firstDateOfCurrentMonth && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else if (j == allFridayDate.Count)
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= lastDateOfCurrentMonth).CountAsync();
                    DateTime dateTime = allFridayDate[j - 1];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, lastDateOfCurrentMonth.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
                else
                {
                    var totalPatietnDateRange = await allData.Where(p => p.CreatedOn >= allFridayDate[j - 1].AddDays(1) && p.CreatedOn.Date <= allFridayDate[j]).CountAsync();
                    DateTime dateTime = allFridayDate[j];
                    WeeklyPatientCountDto weeklyPatientCount = new WeeklyPatientCountDto(totalPatietnDateRange, dateTime.ToString("dd-MM-yy"));
                    weeklyPatientCounts.Add(weeklyPatientCount);
                }
            }

            HomePageReportDto homePageReport = new HomePageReportDto(monthName, totalData, weeklyPatientCounts);

            return homePageReport;
        }

        [HttpGet("usernameandtotalpatient")]
        public async Task<UserNameAndPatientCountDto> GetUserNameAndTotalPatient()
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var doctorName = currentuser.FirstName + " " + currentuser.LastName;
            var totalPatient = await _context.Patient.Where(p => p.CreatedBy == currentuser.Email).CountAsync();
            return new UserNameAndPatientCountDto(doctorName,totalPatient);
        }

        [HttpGet("usernameandtotalprescription")]
        public async Task<UserNameAndPatientCountDto> GetUserNameAndTotalPrescriptiont()
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var doctorName = currentuser.FirstName + " " + currentuser.LastName;
            var totalPatient = await _context.Prescription.Where(p => p.DoctorId == currentuser.Id).CountAsync();
            return new UserNameAndPatientCountDto(doctorName, totalPatient);
        }

        // POST api/<HomePageReportController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomePageReportController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomePageReportController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
