using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public interface IJobOpportunityService
    {
        List<JobOpportunity> JobSearch(JobOpportunity filters);
        Task Post(JobOpportunity request);
        Task PostSuscription(User request);
        List<User> GetSuscriptionUser();
        Task<User> GetUserById(int id);
        Task<Stream> GetExportFile();
        string GetSuscriptionNow();
        Task<int> GetSumValueAndTotal(int value);
        int GetSumValueOneAndValueTwo(int value1, int value2);
        int GetSumValue1Value2Value3Value4(int value1, int value2,int value3, int value4);
        List<JobOpportunity> GetJobInternal(JobOpportunity filters);
        string BodyEmailHtml(User user, string position);
        Task<StreamWriter> CreateFile(string fileName);
        bool ReturnBool(int age);
        IEnumerable<User> ReturnListUser();
        Task DeleteUser(User request);
    }
}