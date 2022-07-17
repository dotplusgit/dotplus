using System;

namespace HospitalAPI.Core.Dtos.PregnancyDto
{
    public class PregnantWomanProfile
    {
        public PregnantWomanProfile(int id, 
                                    string name, 
                                    DateTime firstDateOfLastPeriod, 
                                    DateTime expectedDeliveryDate, 
                                    int firstCheckup, DateTime? firstCheckupdate, 
                                    int secondCheckup, DateTime? secondCheckupdate, 
                                    int thirdCheckup, DateTime? thirdCheckupdate, 
                                    int fourthCheckup, DateTime? fourthCheckupdate, 
                                    int fifthCheckup, DateTime? fifthCheckupdate, 
                                    int sixthCheckup, DateTime? sixthCheckupdate, 
                                    int seventhCheckup, DateTime? seventhCheckupdate, 
                                    int eightthCheckup, DateTime? eightthCheckupdate, 
                                    int ninethCheckup, DateTime? ninethCheckupdate, 
                                    int tenthCheckup, DateTime? tenthCheckupdate)
        {
            Id = id;
            Name = name;
            FirstDateOfLastPeriod = firstDateOfLastPeriod;
            ExpectedDeliveryDate = expectedDeliveryDate;
            FirstCheckup = firstCheckup;
            FirstCheckupdate = firstCheckupdate;
            SecondCheckup = secondCheckup;
            SecondCheckupdate = secondCheckupdate;
            ThirdCheckup = thirdCheckup;
            ThirdCheckupdate = thirdCheckupdate;
            FourthCheckup = fourthCheckup;
            FourthCheckupdate = fourthCheckupdate;
            FifthCheckup = fifthCheckup;
            FifthCheckupdate = fifthCheckupdate;
            SixthCheckup = sixthCheckup;
            SixthCheckupdate = sixthCheckupdate;
            SeventhCheckup = seventhCheckup;
            SeventhCheckupdate = seventhCheckupdate;
            EightthCheckup = eightthCheckup;
            EightthCheckupdate = eightthCheckupdate;
            NinethCheckup = ninethCheckup;
            NinethCheckupdate = ninethCheckupdate;
            TenthCheckup = tenthCheckup;
            TenthCheckupdate = tenthCheckupdate;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDateOfLastPeriod { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public int FirstCheckup { get; set; }
        public DateTime? FirstCheckupdate { get; set; }
        public int SecondCheckup { get; set; }
        public DateTime? SecondCheckupdate { get; set; }
        public int ThirdCheckup { get; set; }
        public DateTime? ThirdCheckupdate { get; set; }
        public int FourthCheckup { get; set; }
        public DateTime? FourthCheckupdate { get; set; }
        public int FifthCheckup { get; set; }
        public DateTime? FifthCheckupdate { get; set; }
        public int SixthCheckup { get; set; }
        public DateTime? SixthCheckupdate { get; set; }
        public int SeventhCheckup { get; set; }
        public DateTime? SeventhCheckupdate { get; set; }
        public int EightthCheckup { get; set; }
        public DateTime? EightthCheckupdate { get; set; }
        public int NinethCheckup { get; set; }
        public DateTime? NinethCheckupdate { get; set; }
        public int TenthCheckup { get; set; }
        public DateTime? TenthCheckupdate { get; set; }
    }
}
