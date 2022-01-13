using System.Collections.Generic;
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
    }
}