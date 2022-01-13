using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiUnitest.ApiJob.Api.Repository.Repositories
{
    public interface IJobOpportunityRepository
    {
        Task Post (JobOpportunity job);
        Task<IQueryable<User>> GetUsersFilter(string filter);
        List<JobOpportunity> GetJobOpportunitiesList();
        List<User> GetUsersList();
        Task PostUser(User request);
    }
}