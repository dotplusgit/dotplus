using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.Core.Models.FollowUpModel;
using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.Core.Models.MedicinePurchaseAggregate;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.Core.Models.TelemedicineModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HospitalAPI.DataAccess.Data
{
    public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Upazila> Upazila { get; set; }
        public DbSet<VisitEntry> VisitEntry { get; set; }
        public DbSet<PhysicalState> PhysicalState { get; set; }
        public DbSet<DiseasesCategory> DiseasesCategory { get; set; }
        public DbSet<Diseases> Diseases { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<MedicineStock> MedicineStock { get; set; }
        public DbSet<MedicinePurchasePerUnit> MedicinePurchasePerUnit { get; set; }
        public DbSet<MedicinePurchase> MedicinePurchase { get; set; }
        public DbSet<MedicineManufacturar> MedicineManufacturar { get; set; }
        public DbSet<MedicineForPrescription> MedicineForPrescription { get; set; }
        public DbSet<Followup> Followup { get; set; }
        public DbSet<Telemedicine> Telemedicine { get; set; }
        public DbSet<Pregnancy> Pregnancy { get; set; }
        public DbSet<MonthlyCheckupPregnancy> MonthlyCheckupPregnancy { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PhysicalState>(v => 
            {
                v.HasOne(p => p.Patient)
                .WithMany(v => v.PhysicalStates)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Patient>(p =>
            {
                p.HasMany(b => b.PhysicalStates)
                    .WithOne(p => p.Patient)
                    .HasForeignKey(p => p.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Patient>(v =>
            {
                v.HasOne(p => p.District)
                .WithMany()
                .HasForeignKey(f => f.DistrictId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Patient>(v =>
            {
                v.HasOne(p => p.Upazila)
                .WithMany()
                .HasForeignKey(f => f.UpazilaId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<VisitEntry>(v =>
            {
                v.HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<PhysicalState>(v =>
            {
                v.HasOne(p => p.VisitEntry)
                .WithMany()
                .HasForeignKey(f => f.VisitEntryId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Prescription>(v =>
            {
                v.HasOne(p => p.Patient)
                .WithMany()
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Prescription>(v =>
            {
                v.HasOne(p => p.VisitEntry)
                .WithMany()
                .HasForeignKey(f => f.VisitEntryId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<MedicinePurchase>(v =>
            {
                v.HasOne(p => p.Prescription)
                .WithMany()
                .HasForeignKey(f => f.PrescriptionId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
