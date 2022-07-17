using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Helpers
{
    public static class ReportLog
    {
        public static void WriteTextFile(string requestName,
               string userName,
               string userEmail,
               string userHospital,
               string requestOrResponse,
               string status,
               string statusDetails = "Ok")
        {
            string rquestOrResponseTime = DateTime.Now.ToString("dd/MM/yyyy-hh:mm:ss");
            string folderName = Path.Combine("Resources", "TextFile");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string FileName =  $"ReportLog{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
            string pathToSaveWithFileName = Path.Combine(pathToSave, FileName); 

            string line = $"=> Request Name: {requestName}, Name: {userName}, email: {userEmail}, Hospital: {userHospital}, {requestOrResponse}: {rquestOrResponseTime},\n...Status:{status}, {statusDetails}";
            StreamWriter sw = new(pathToSaveWithFileName, append: true);
            sw.WriteLine(line);
            sw.Close();
        }
    }
}
