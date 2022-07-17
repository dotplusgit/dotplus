using HospitalAPI.DataAccess.Repository;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Helpers;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IVisitEntryRepository, VisitEntryRepository>();
            services.AddScoped<IPhysicalStateRepository, PhysicalStateRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMedicineStockRepository, MedicineStockRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IFollowupRepository, FollowupRepository>();
            services.AddScoped<ITelemedicineRepository, TelemedicineRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));
            // Email Service Providing Section 
            services.AddTransient<IEmailSender, EmailSender>();
            //Generic Repo.
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            return services;
        }
    }
}
