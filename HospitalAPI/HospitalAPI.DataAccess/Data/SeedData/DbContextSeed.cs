using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.FollowUpModel;
using HospitalAPI.DataAccess.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Data.SeedData
{
    public static class DbContextSeed
    {
            public static async Task SeedHospitalAsync(ApplicationDbContext context)
            {
                if (!context.Hospital.Any())
                {
                //Seed Hospital
                    var hospital = new Hospital
                    {
                        Name = "System",
                        Address = "system",
                        UpazilaId = null,
                        DistrictId = null,
                        IsActive = true,
                        CreatedOn = DateTime.Now,
                        UpdatedOn = DateTime.Now,
                        CreatedBy = "system",
                        UpdatedBy = "system"
                    };
                    context.Hospital.Add(hospital);
                    await context.SaveChangesAsync();
                }
            }
        //seed followup
        public static async Task SeedFollowupAsync(ApplicationDbContext context)
        {
            var patientsThoseHaveFolloupDate = await context.Prescription.Where(p => p.NextVisit != null).ToListAsync();

            if (patientsThoseHaveFolloupDate.Any())
            {
                foreach(var prescription in patientsThoseHaveFolloupDate)
                {
                    var followUpExist = context.Followup.Where(f => f.PrescriptionId == prescription.Id).Any();
                    if(followUpExist)
                    {
                        continue;
                    }
                    else
                    {
                        var thisPatientPreviousFollowUp = await context.Followup.Where(p => p.PatientId == prescription.PatientId).ToListAsync();

                        if (thisPatientPreviousFollowUp.Any())
                        {
                            foreach (var folowup in thisPatientPreviousFollowUp)
                            {
                                folowup.IsFollowup = true;
                                context.Followup.Update(folowup);
                                await context.SaveChangesAsync();
                            }
                        }
                        var followUp = new Followup(prescription.PatientId, prescription.DoctorId, prescription.Id, prescription.HospitalId, (DateTime)prescription.NextVisit, false);
                        context.Followup.Add(followUp);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        //seed User
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
            {
                if (!userManager.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        HospitalId = 1,
                        FirstName = "admin",
                        UserName = "admin@admin.com",
                        LastName = "admin",
                        Email = "admin@admin.com",
                        Designation ="admin",
                        IsActive = true,
                        Role = Role.Admin
                    };

                    IdentityResult result = await userManager.CreateAsync(user, "P@$$w0rd1234d");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Role.Admin);
                    }
                }
            }

        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            { 
                //Seed Roles
                await roleManager.CreateAsync(new ApplicationRole 
                { 
                Name = Role.Admin,
                IsActive = true
                });
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Role.FrontDesk,
                    IsActive = true
                });
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Role.Doctor,
                    IsActive = true
                });
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = Role.Pharmacist,
                    IsActive = true
                });
            }

        }

        // seed physical stat
        public static async Task SeedPhysicalstatToPrescriptionAsync(ApplicationDbContext context)
        {
            var prescriptionWithoutPhysicalStat = await context.Prescription.Where(p => p.PhysicalStateId == null).ToListAsync();

            if (prescriptionWithoutPhysicalStat.Any())
            {
                foreach (var prescription in prescriptionWithoutPhysicalStat)
                {
                    var thisDatePhysicalStat = await context.PhysicalState.Where(p => p.PatientId == prescription.PatientId
                                                                                && p.CreatedOn.Date == prescription.CreatedOn.Date)
                                                                          .OrderByDescending(p => p.CreatedOn)
                                                                          .FirstOrDefaultAsync();
                    if (thisDatePhysicalStat == null)
                    {
                        continue;
                    }
                    else
                    {
                        prescription.PhysicalStateId = thisDatePhysicalStat.Id;
                        context.Prescription.Update(prescription);
                        //await context.SaveChangesAsync();
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
