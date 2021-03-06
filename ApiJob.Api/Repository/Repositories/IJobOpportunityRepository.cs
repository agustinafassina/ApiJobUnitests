using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiUnitest.ApiJob.Api.Repository.Repositories
{
    public interface IJobOpportunityRepository
    {
        Task PostJobs (JobOpportunity job);
        Task<IQueryable<User>> GetUsersFilter(string filter);
        List<JobOpportunity> GetJobOpportunitiesList();
        List<User> GetUsersList();
        Task PostUser(User request);
        Task<User> GetUserById(int id);
        Task DeleteUser(User request);
    }
}