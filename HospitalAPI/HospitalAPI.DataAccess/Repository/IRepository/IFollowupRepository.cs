using HospitalAPI.Core.Models.FollowUpModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IFollowupRepository
    {
        IQueryable<Followup> PendingFollowupRecordList(int hospitalId, string search);
        Task<IReadOnlyList<Followup>> IndividualPatientFollowUpRecordList(int patientId);
        Task<IReadOnlyList<Followup>> IndividualPatientPendingFollowUpRecordList(int patientId);
    }
}
