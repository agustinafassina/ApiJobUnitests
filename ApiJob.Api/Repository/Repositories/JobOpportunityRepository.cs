using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Repository.Context;

namespace ApiUnitest.ApiJob.Api.Repository.Repositories
{
    public class JobOpportunityRepository : IJobOpportunityRepository
    {
        private readonly ApiDBContext _context;
        public JobOpportunityRepository(ApiDBContext context)
        {
            _context = context;
        }

        public async Task PostJobs(JobOpportunity request)
        {
            try{
                _context.Add(request);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task<IQueryable<User>> GetUsersFilter(string filter)
        {
            return Task.FromResult(_context.Users.Where(x => x.InterestPositionsName == filter));
        }

        public List<JobOpportunity> GetJobOpportunitiesList()
        {
            return _context.JobOpportunities.ToList();
        }

        public List<User> GetUsersList()
        {
            return _context.Users.ToList();
        }

        public async Task PostUser(User request)
        {
            try{
                _context.Add(request);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task<User> GetUserById(int id)
        {
            return (Task<User>)_context.Users.Where(x => x.Id == id);
        }
    }
}