using AutoMapper;
using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Dtos.BranchDto;
using HospitalAPI.Core.Dtos.DignosisDto;
using HospitalAPI.Core.Dtos.FollowUpDto;
using HospitalAPI.Core.Dtos.MedicineDto;
using HospitalAPI.Core.Dtos.PatientDto;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Dtos.PregnancyDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using HospitalAPI.Core.Dtos.TelemedicineDto;
using HospitalAPI.Core.Dtos.UserBasedDto;
using HospitalAPI.Core.Dtos.VisitEntryDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.Core.Models.FollowUpModel;
using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.Core.Models.TelemedicineModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using HospitalAPI.Dto.PatientDto;

namespace HospitalAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //User
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(i => i.Id))
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name));
            CreateMap<ApplicationUser, UpdateUserDto>();
            //Role
            CreateMap<ApplicationRole, GetRoleDto>()
                .ForMember(dest => dest.RoleName, src => src.MapFrom(i => i.Name));
            //Branch
            CreateMap<Branch, GetBranchDto>()
                .ForMember(dest => dest.Division, src => src.MapFrom(i => i.Division.Name))
                .ForMember(dest => dest.District, src => src.MapFrom(i => i.District.Name))
                .ForMember(dest => dest.Upazila, src => src.MapFrom(i => i.Upazila.Name));
            CreateMap<Branch, GetBranchDtoSortByName>();
            //Hospital
            CreateMap<Hospital, HospitalGetDto>()
                .ForMember(dest => dest.Branch, src => src.MapFrom(i => i.Branch.Name))
                .ForMember(dest => dest.Division, src => src.MapFrom(i => i.Division.Name))
                .ForMember(dest => dest.District, src => src.MapFrom(i => i.District.Name))
                .ForMember(dest => dest.Upazila, src => src.MapFrom(i => i.Upazila.Name));
            CreateMap<Hospital, HospitalGetDtoShortByName>();
            //Patient
            CreateMap<Patient, GetPatientDto>()
                .ForMember(dest => dest.BranchName, src => src.MapFrom(i => i.Branch.Name))
                .ForMember(dest => dest.DivisionId, src => src.MapFrom(i => i.Division.Id))
                .ForMember(dest => dest.Division, src => src.MapFrom(i => i.Division.Name))
                .ForMember(dest => dest.Upazila, src => src.MapFrom(i => i.Upazila.Name))
                .ForMember(dest => dest.District, src => src.MapFrom(i => i.District.Name))
                .ForMember(dest => dest.UpazilaId, src => src.MapFrom(i => i.Upazila.Id))
                .ForMember(dest => dest.DistrictId, src => src.MapFrom(i => i.District.Id))
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name));

            CreateMap<Patient, GetPatientfromApiDto>()
                .ForMember(dest => dest.BranchName, src => src.MapFrom(i => i.Branch.Name))
                .ForMember(dest => dest.DivisionId, src => src.MapFrom(i => i.Division.Id))
                .ForMember(dest => dest.Division, src => src.MapFrom(i => i.Division.Name))
                .ForMember(dest => dest.Upazila, src => src.MapFrom(i => i.Upazila.Name))
                .ForMember(dest => dest.District, src => src.MapFrom(i => i.District.Name))
                .ForMember(dest => dest.UpazilaId, src => src.MapFrom(i => i.Upazila.Id))
                .ForMember(dest => dest.DistrictId, src => src.MapFrom(i => i.District.Id))
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name));

            CreateMap<Patient, GetPatientWithPhysicalStatDto>()
                .ForMember(dest => dest.BranchName, src => src.MapFrom(i => i.Branch.Name))
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name))
                .ForMember(dest => dest.DivisionId, src => src.MapFrom(i => i.Division.Id))
                .ForMember(dest => dest.Division, src => src.MapFrom(i => i.Division.Name))
                .ForMember(dest => dest.Upazila, src => src.MapFrom(i => i.Upazila.Name))
                .ForMember(dest => dest.District, src => src.MapFrom(i => i.District.Name))
                .ForMember(dest => dest.UpazilaId, src => src.MapFrom(i => i.Upazila.Id))
                .ForMember(dest => dest.DistrictId, src => src.MapFrom(i => i.District.Id))
                .ForMember(dest => dest.PhysicalStat, src => src.MapFrom(i => i.PhysicalStates));
            CreateMap<Patient, GetPatientForSearch>()
                .ForMember(dest => dest.Id, src => src.MapFrom(i => i.Id))
                .ForMember(dest => dest.MobileNumber, src => src.MapFrom(i => i.MobileNumber));
            //Visit Entry
            CreateMap<VisitEntry, GetVisitEntryDto>()
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name))
                .ForMember(dest => dest.PatientFirstName, src => src.MapFrom(i => i.Patient.FirstName))
                .ForMember(dest => dest.PatientLastName, src => src.MapFrom(i => i.Patient.LastName));
            //Physical State
            CreateMap<PhysicalState, GetPhysicalStateDto>()
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name))
                .ForMember(dest => dest.PatientFirstName, src => src.MapFrom(i => i.Patient.FirstName))
                .ForMember(dest => dest.PatientLastName, src => src.MapFrom(i => i.Patient.LastName));
            //Prescription
            CreateMap<Prescription, GetPrescriptionDto>()
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name))
                .ForMember(dest => dest.PatientFirstName, src => src.MapFrom(i => i.Patient.FirstName))
                .ForMember(dest => dest.PatientLastName, src => src.MapFrom(i => i.Patient.LastName))
                .ForMember(dest => dest.PatientDob, src => src.MapFrom(i => i.Patient.DoB))
                //.ForMember(dest => dest.PhysicalStat, src => src.MapFrom(i => i.Patient.PhysicalStates))
                .ForMember(dest => dest.PhysicalStat, src => src.MapFrom(i => i.PhysicalState))
                .ForMember(dest => dest.PatientBloodGroup, src => src.MapFrom(i => i.Patient.BloodGroup))
                .ForMember(dest => dest.PatientGender, src => src.MapFrom(i => i.Patient.Gender))
                .ForMember(dest => dest.PatientMobile, src => src.MapFrom(i => i.Patient.MobileNumber))
                .ForMember(dest => dest.Covidvaccine, src => src.MapFrom(i => i.Patient.Covidvaccine))
                .ForMember(dest => dest.VaccineBrand, src => src.MapFrom(i => i.Patient.VaccineBrand))
                .ForMember(dest => dest.VaccineDose, src => src.MapFrom(i => i.Patient.VaccineDose))
                .ForMember(dest => dest.Diagnosis, src => src.MapFrom(i => i.Diagnosis))
                .ForMember(dest => dest.MedicineForPrescription, src => src.MapFrom(i => i.MedicineForPrescription));


            //Medicine
            CreateMap<Medicine, GetMedicineDto>()
                .ForMember(dest => dest.Manufacturar, src => src.MapFrom(i => i.MedicineManufacturar.Name))
                .ForMember(dest => dest.ManufacturarId, src => src.MapFrom(i => i.MedicineManufacturar.Id));
            // GetMedicineForPrescription
            CreateMap<Medicine, GetMedicineForPrescription>()
                .ForMember(dest => dest.Manufacturar, src => src.MapFrom(i => i.MedicineManufacturar.Name));
            // GetMedicinesync
            CreateMap<Medicine, GetMedicineForSync>();
            //MedicineStock
            CreateMap<MedicineStock, GetMedicineStockDto>()
                .ForMember(dest => dest.HospitalName, src => src.MapFrom(i => i.Hospital.Name));
            CreateMap<MedicineForPrescription, GetMedicineForPrescriptionDto>()
                .ForMember(dest => dest.MedicineId, src => src.MapFrom(i => i.MedicineId))
                .ForMember(dest => dest.MedicineType, src => src.MapFrom(i => i.Medicine.MedicineType))
                .ForMember(dest => dest.BrandName, src => src.MapFrom(i => i.Medicine.BrandName))
                .ForMember(dest => dest.GenericName, src => src.MapFrom(i => i.Medicine.GenericName));
            //FollowUp
            CreateMap<Followup, GetFollowUpListDto>()
                .ForMember(dest => dest.PatientId, src => src.MapFrom(i => i.Patient.Id))
                .ForMember(dest => dest.PatientFirstName, src => src.MapFrom(i => i.Patient.FirstName))
                .ForMember(dest => dest.PatientLastName, src => src.MapFrom(i => i.Patient.LastName))
                .ForMember(dest => dest.PatientMobileNumber, src => src.MapFrom(i => i.Patient.MobileNumber))
                .ForMember(dest => dest.FollowupDate, src => src.MapFrom(i => i.FollowupDate))
                .ForMember(dest => dest.DoctorFirstName, src => src.MapFrom(i => i.ApplicationUser.FirstName))
                .ForMember(dest => dest.DoctorLastName, src => src.MapFrom(i => i.ApplicationUser.LastName))
                .ForMember(dest => dest.LastVisitHospital, src => src.MapFrom(i => i.Prescription.Hospital.Name))
                .ForMember(dest => dest.LastVisitDate, src => src.MapFrom(i => i.Prescription.UpdatedOn));
            //Diagnosis
            CreateMap<Diagnosis, GetDiagnosisDto>()
                .ForMember(dest => dest.PatientFristName, src => src.MapFrom(i => i.Patient.FirstName))
                .ForMember(dest => dest.PatientLastName, src => src.MapFrom(i => i.Patient.LastName))
                .ForMember(dest => dest.DiseasesCategory, src => src.MapFrom(i => i.DiseasesCategory.Name))
                .ForMember(dest => dest.Diseases, src => src.MapFrom(i => i.Diseases));
            //Telemedicine
            CreateMap<Telemedicine, GetTelemedicineDto>()
                .ForMember(dest => dest.CallerFirstName, src => src.MapFrom(i => i.Caller.FirstName))
                .ForMember(dest => dest.CallerLastName, src => src.MapFrom(i => i.Caller.LastName))
                .ForMember(dest => dest.ReceiverFirstName, src => src.MapFrom(i => i.Receiver.FirstName))
                .ForMember(dest => dest.ReceiverLastName, src => src.MapFrom(i => i.Receiver.LastName))
                .ForMember(dest => dest.ReceiverPhoneNumber, src => src.MapFrom(i => i.Receiver.PhoneNumber));
            CreateMap<Pregnancy, GetPregnancyDto>()
                .ForMember(dest => dest.Name, src => src.MapFrom(i => i.Patient.FirstName + " " + i.Patient.LastName));
        }
    }
}
//public string PatientFirstName { get; set; }
//public string PatientLastName { get; set; }
//public string PatientMobileNumber { get; set; }
//public DateTime FollowupDate { get; set; }
//public string DoctorFirstName { get; set; }
//public string DoctorLastName { get; set; }
//public string LastVisitHospital { get; set; }
//public DateTime LastVisitDate { get; set; }
//public int? PrescriptionId { get; set; }
//public bool IsFollowup { get; set; }