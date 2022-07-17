namespace HospitalAPI.Dto
{
    public class UserNameAndPatientCountDto
    {
        public UserNameAndPatientCountDto(string name, int patientCount)
        {
            Name = name;
            PatientCount = patientCount;
        }

        public string Name { get; set; }
        public int PatientCount { get; set; }
    }
}
