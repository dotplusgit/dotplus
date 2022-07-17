using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.Core.Models.DiagnosisReportModel;
using HospitalAPI.Core.StaticMethos;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Extensions;
using HospitalAPI.Helpers;
using Microsoft.AspNetCore.Http;
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
    public class DiagnosisReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiagnosisReportController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public IReadOnlyList<DiagnosisReportDto> GetPatientAccordingToBranchBetweenTwoDates(int month, int year, int hospitalId)
        {
            IQueryable<Diagnosis> diagnoses = _context.Diagnosis
                                                      .Include(d => d.DiseasesCategory)
                                                      .Include(d => d.Patient)
                                                      .Include(d => d.Prescription);
            IQueryable<Diagnosis> diagnosesWithSpecification = diagnoses.Where(d => d.Prescription.UpdatedOn.Year == year && 
                                                                                    d.Prescription.UpdatedOn.Month == month && 
                                                                                    d.Prescription.HospitalId == hospitalId);

            int currentYear = DateTime.Now.Date.Year;
            var groupByDiseases = diagnosesWithSpecification.ToList().GroupBy(dCat => dCat.DiseasesCategoryId)
                                                                               .Select(x => new
                                                                               {
                                                                                   DiseasesCategoryId = x.Key,
                                                                                   Gender = x.GroupBy(p => p.Patient.Gender).Select(y => new
                                                                                   {
                                                                                       Gender = y.Key,
                                                                                       ZeroToFive = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 0 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 5),
                                                                                       SixToFifteen = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 6 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 15),
                                                                                       SixteenToThirty = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 16 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 30),
                                                                                       ThirtyOneToFourtyFive = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 31 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 45),
                                                                                       FourtySixToSixty = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 46 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 60),
                                                                                       SixtyOnePlus = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) > 60),
                                                                                       Total = y.Count()
                                                                                   }).ToList(),
                                                                                   TotalPatient = x.Count(p => p.Patient.Id > 0)
                                                                               }).ToList();

            List<DiagnosisReportDto> result = new();
            foreach (var diagnosiss in groupByDiseases)
            {
                var comulativeUpToThisMonth = diagnoses.Where(d => d.DiseasesCategoryId == diagnosiss.DiseasesCategoryId).Count();
                var diseasesCategory = _context.DiseasesCategory.FirstOrDefault(d => d.Id == diagnosiss.DiseasesCategoryId);
                ReportByAge reportByAgeInit = new()
                {
                    ZeroToFive = 0,
                    SixToFifteen = 0,
                    SixteenToThirty = 0,
                    ThirtyOneToFourtyFive = 0,
                    FourtyFiveToSixty = 0,
                    SixtyPlus = 0,
                    Total = 0
                };
                GenderMale genderMale = new()
                {
                    Name = "",
                    Ages = reportByAgeInit
                }; 
                GenderFeMale genderFeMale = new()
                {
                    Name = "",
                    Ages = reportByAgeInit
                };
                GenderOthers genderOthers = new()
                {
                    Name = "",
                    Ages = reportByAgeInit
                };
                foreach (var gen in diagnosiss.Gender)
                {
                    if(gen.Gender == "Male")
                    {
                        ReportByAge reportByAge = new()
                        {
                            ZeroToFive = gen.ZeroToFive,
                            SixToFifteen = gen.SixToFifteen,
                            SixteenToThirty = gen.SixteenToThirty,
                            ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                            FourtyFiveToSixty = gen.FourtySixToSixty,
                            SixtyPlus = gen.SixtyOnePlus,
                            Total = gen.Total
                        };
                        GenderMale gender = new()
                        {
                            Name = gen.Gender,
                            Ages = reportByAge
                        };
                        genderMale = gender;
                    }
                    else if(gen.Gender == "Female")
                    {
                        ReportByAge reportByAge = new()
                        {
                            ZeroToFive = gen.ZeroToFive,
                            SixToFifteen = gen.SixToFifteen,
                            SixteenToThirty = gen.SixteenToThirty,
                            ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                            FourtyFiveToSixty = gen.FourtySixToSixty,
                            SixtyPlus = gen.SixtyOnePlus,
                            Total = gen.Total
                        };
                        GenderFeMale gender = new()
                        {
                            Name = gen.Gender,
                            Ages = reportByAge
                        };
                        genderFeMale = gender;
                    }
                    else
                    {
                        ReportByAge reportByAge = new()
                        {
                            ZeroToFive = gen.ZeroToFive,
                            SixToFifteen = gen.SixToFifteen,
                            SixteenToThirty = gen.SixteenToThirty,
                            ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                            FourtyFiveToSixty = gen.FourtySixToSixty,
                            SixtyPlus = gen.SixtyOnePlus,
                            Total = gen.Total
                        };
                        GenderOthers gender = new()
                        {
                            Name = gen.Gender,
                            Ages = reportByAge
                        };
                        genderOthers = gender;
                    }
                }
                DiagnosisReportDto diagnosisReport = new()
                {
                    ComulativeUpToThisMonth = comulativeUpToThisMonth,
                    CategoryId = diagnosiss.DiseasesCategoryId,
                    CategoryName = diseasesCategory != null ? diseasesCategory.Name : " ",
                    GenderMale = genderMale,
                    GenderFemale = genderFeMale,
                    GenderOthers = genderOthers,
                    Total = diagnosiss.TotalPatient,
                    UpToPreviousMonth = comulativeUpToThisMonth - diagnosiss.TotalPatient
                };
                result.Add(diagnosisReport);
            }
            return result;
        }
        [HttpGet("diagnosiscategoryreport")]
        public async Task<DiagnosisReportWithSpecifcation> DiagnosisCategoryReport(int month, int year, int? hospitalId)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            // ReportLog.WriteTextFile("Diagnosis Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Request", "Ok");
            var hospital = await _context.Hospital.FirstOrDefaultAsync(b => b.Id == hospitalId);

            DateTime thisDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            string monthName = MonthName.GetMonth(month);
            int currentYear = DateTime.Now.Date.Year;
            IQueryable<Diagnosis> diagnoses = _context.Diagnosis
                                                      .Include(d => d.DiseasesCategory)
                                                      .Include(d => d.Patient)
                                                      .Include(d => d.Prescription);
            IQueryable<Diagnosis> diagnosesWithSpecificHospital;
            IQueryable<Diagnosis> diagnosesWithSpecification;
            if (hospitalId == null || hospitalId < 1)
            {
                 diagnosesWithSpecificHospital = diagnoses.Where(d => d.Prescription.UpdatedOn.Date <= thisDate.Date);
                 diagnosesWithSpecification = diagnoses.Where(d => d.Prescription.UpdatedOn.Year == year &&
                                                                   d.Prescription.UpdatedOn.Month == month);
            }
            else
            {
                 diagnosesWithSpecificHospital = diagnoses.Where(p => p.Prescription.HospitalId == hospitalId && 
                                                                      p.Prescription.UpdatedOn.Date <= thisDate.Date);
                 diagnosesWithSpecification = diagnoses.Where(d => d.Prescription.UpdatedOn.Year == year && 
                                                              d.Prescription.UpdatedOn.Month == month &&
                                                              d.Prescription.HospitalId == hospitalId);
            }

            IReadOnlyList<DiseasesCategory> diseasesCategories =await _context.DiseasesCategory.ToListAsync();
            List<DiagnosisReportDto> result = new();
            foreach (var diseasesCategory in diseasesCategories)
            {
                var thisDiseasesCategory = _context.DiseasesCategory.FirstOrDefault(d => d.Id == diseasesCategory.Id);
                var comulativeUpToThisMonth = diagnosesWithSpecificHospital.Where(d => d.DiseasesCategoryId == diseasesCategory.Id).Count();
                int currentMonthTotal = diagnosesWithSpecification.Where(d => d.DiseasesCategoryId == diseasesCategory.Id).Count();
                var thisMonthDiagnosisUnderThisDiseasesCategory =await diagnosesWithSpecification.Where(d => d.DiseasesCategoryId == diseasesCategory.Id).ToListAsync();
                if (thisMonthDiagnosisUnderThisDiseasesCategory != null)
                {
                    var groupByGender = thisMonthDiagnosisUnderThisDiseasesCategory.GroupBy(p => p.Patient.Gender).Select(y => new
                    {
                        Gender = y.Key,
                        ZeroToFive = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 0 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 5),
                        SixToFifteen = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 6 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 15),
                        SixteenToThirty = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 16 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 30),
                        ThirtyOneToFourtyFive = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 31 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 45),
                        FourtySixToSixty = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) >= 46 && (currentYear - p.Patient.DoB.Value.Date.Year) <= 60),
                        SixtyOnePlus = y.Count(p => (currentYear - p.Patient.DoB.Value.Date.Year) > 60),
                        Total = y.Count()
                    }).ToList();
                    ReportByAge reportByAgeInit = new()
                    {
                        ZeroToFive = 0,
                        SixToFifteen = 0,
                        SixteenToThirty = 0,
                        ThirtyOneToFourtyFive = 0,
                        FourtyFiveToSixty = 0,
                        SixtyPlus = 0,
                        Total = 0
                    };
                    GenderMale genderMale = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    GenderFeMale genderFeMale = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    GenderOthers genderOthers = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    foreach (var gen in groupByGender)
                    {
                        if (gen.Gender == "Male")
                        {
                            ReportByAge reportByAge = new()
                            {
                                ZeroToFive = gen.ZeroToFive,
                                SixToFifteen = gen.SixToFifteen,
                                SixteenToThirty = gen.SixteenToThirty,
                                ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                                FourtyFiveToSixty = gen.FourtySixToSixty,
                                SixtyPlus = gen.SixtyOnePlus,
                                Total = gen.Total
                            };
                            GenderMale gender = new()
                            {
                                Name = gen.Gender,
                                Ages = reportByAge
                            };
                            genderMale = gender;
                        }
                        else if (gen.Gender == "Female")
                        {
                            ReportByAge reportByAge = new()
                            {
                                ZeroToFive = gen.ZeroToFive,
                                SixToFifteen = gen.SixToFifteen,
                                SixteenToThirty = gen.SixteenToThirty,
                                ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                                FourtyFiveToSixty = gen.FourtySixToSixty,
                                SixtyPlus = gen.SixtyOnePlus,
                                Total = gen.Total
                            };
                            GenderFeMale gender = new()
                            {
                                Name = gen.Gender,
                                Ages = reportByAge
                            };
                            genderFeMale = gender;
                        }
                        else
                        {
                            ReportByAge reportByAge = new()
                            {
                                ZeroToFive = gen.ZeroToFive,
                                SixToFifteen = gen.SixToFifteen,
                                SixteenToThirty = gen.SixteenToThirty,
                                ThirtyOneToFourtyFive = gen.ThirtyOneToFourtyFive,
                                FourtyFiveToSixty = gen.FourtySixToSixty,
                                SixtyPlus = gen.SixtyOnePlus,
                                Total = gen.Total
                            };
                            GenderOthers gender = new()
                            {
                                Name = gen.Gender,
                                Ages = reportByAge
                            };
                            genderOthers = gender;
                        }
                    }
                    DiagnosisReportDto diagnosisReport = new()
                    {
                        ComulativeUpToThisMonth = comulativeUpToThisMonth,
                        CategoryId = thisDiseasesCategory.Id,
                        CategoryName = diseasesCategory != null ? diseasesCategory.Name : " ",
                        GenderMale = genderMale,
                        GenderFemale = genderFeMale,
                        GenderOthers = genderOthers,
                        Total = currentMonthTotal,
                        UpToPreviousMonth = comulativeUpToThisMonth - currentMonthTotal
                    };
                    result.Add(diagnosisReport);
                }
                else
                {
                    ReportByAge reportByAgeInit = new()
                    {
                        ZeroToFive = 0,
                        SixToFifteen = 0,
                        SixteenToThirty = 0,
                        ThirtyOneToFourtyFive = 0,
                        FourtyFiveToSixty = 0,
                        SixtyPlus = 0,
                        Total = 0
                    };
                    GenderMale genderMale = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    GenderFeMale genderFeMale = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    GenderOthers genderOthers = new()
                    {
                        Name = "",
                        Ages = reportByAgeInit
                    };
                    DiagnosisReportDto diagnosisReport = new()
                    {
                        ComulativeUpToThisMonth = comulativeUpToThisMonth,
                        CategoryId = thisDiseasesCategory.Id,
                        CategoryName = diseasesCategory != null ? diseasesCategory.Name : " ",
                        GenderMale = genderMale,
                        GenderFemale = genderFeMale,
                        GenderOthers = genderOthers,
                        Total = currentMonthTotal,
                        UpToPreviousMonth = comulativeUpToThisMonth - currentMonthTotal
                    };
                    result.Add(diagnosisReport);
                }
            }

            DiagnosisReportWithSpecifcation diagnosisReportWithSpecifcation = new()
            {
                FirstName = currentuser.FirstName,
                LastName = currentuser.LastName,
                Designation = currentuser.Designation,
                HospitalId = hospital == null ? 0 : hospital.Id,
                HospitalName = hospital == null ? "All Branch" : hospital.Name,
                MonthName = monthName,
                Year = year,
                DiagnosisReport = result,
                UpToPreviousMonthTotal = result.Sum(r =>r.UpToPreviousMonth),
                ComulativeUpToThisMonthTotal = result.Sum(r => r.ComulativeUpToThisMonth),
                FMFourtyFiveToSixtyTotal = result.Sum(r => r.GenderFemale.Ages.FourtyFiveToSixty),
                FMSixteenToThirtyTotal = result.Sum(r => r.GenderFemale.Ages.SixteenToThirty),
                FMSixToFifteenTotal = result.Sum(r => r.GenderFemale.Ages.SixToFifteen),
                FMSixtyPlusTotal = result.Sum(r => r.GenderFemale.Ages.SixtyPlus),
                FMThirtyOneToFourtyFiveTotal = result.Sum(r => r.GenderFemale.Ages.ThirtyOneToFourtyFive),
                FMZeroToFiveTotal = result.Sum(r => r.GenderFemale.Ages.ZeroToFive),
                MFourtyFiveToSixtyTotal = result.Sum(r => r.GenderMale.Ages.FourtyFiveToSixty),
                MSixteenToThirtyTotal = result.Sum(r => r.GenderMale.Ages.SixteenToThirty),
                MSixToFifteenTotal = result.Sum(r => r.GenderMale.Ages.SixToFifteen),
                MSixtyPlusTotal = result.Sum(r => r.GenderMale.Ages.SixtyPlus),
                MThirtyOneToFourtyFiveTotal = result.Sum(r => r.GenderMale.Ages.ThirtyOneToFourtyFive),
                MZeroToFiveTotal = result.Sum(r => r.GenderMale.Ages.ZeroToFive),
                FMAgeGroupTotal = result.Sum(r => r.GenderFemale.Ages.Total),
                MAgeGroupTotal = result.Sum(r => r.GenderMale.Ages.Total),
                ThisMonthTotal = result.Sum(r => r.Total)
            };
            // ReportLog.WriteTextFile("Diagnosis Report", currentuser.FirstName + " " + currentuser.LastName, currentuser.Email, currentuser.HospitalName, "Response", "Ok");
            return diagnosisReportWithSpecifcation;
        }
    }
}
