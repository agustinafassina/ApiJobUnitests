using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Services;
using Moq;
using NUnit.Framework;
using System.Threading;
using ApiJobUnitests.ApiJob.Api.Repository.Context;
using ApiUnitest.ApiJob.Api.Repository.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace ApiJobUnitests.ApiJob.Tests.Services
{
    public class JobOpportunityServiceTest
    {
        private readonly IJobOpportunityService _jobOpportunityService;
        private readonly Mock<IJobOpportunityRepository> _jobOpportunityRepositoryy;
        public JobOpportunityServiceTest()
        {
            _jobOpportunityRepositoryy = new Mock<IJobOpportunityRepository>(MockBehavior.Strict);
            _jobOpportunityService = new JobOpportunityService(_jobOpportunityRepositoryy.Object);
        }

        [Test, Order(1)]
        public async Task InsertJobOpportunity_ShouldReturnTaskOk()
        {
            var job = new JobOpportunity {
                Id = 1,
                Name = "Database Analyst",
                Country = "Argentina",
                Salary= 15000
            };

            var jobs = CreateJobs().AsQueryable();

            _jobOpportunityRepositoryy.Setup(x => x.Post(It.IsAny<JobOpportunity>()))
                .Returns(Task.CompletedTask);

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersFilter(It.Is<string>(p => p.Equals(job.Name))))
                .Returns(Task.FromResult<IQueryable<User>>(jobs));

            await _jobOpportunityService.Post(job);

            VerifyAll();
        }

        private void VerifyAll()
        {
            _jobOpportunityRepositoryy.VerifyAll();
        }

        private IEnumerable<User> CreateJobs()
        {
            return new List<User>{
                new User {
                    Id = 1,
                    UserName = "test01@unitest.com",
                    InterestPositionsName = "Database Analyst"
                },
                new User {
                    Id = 2,
                    UserName = "test02@unitest.com",
                    InterestPositionsName = "Backend developer"
                },
                new User {
                    Id = 3,
                    UserName = "test03@unitest.com",
                    InterestPositionsName = "Frontend developer"
                },
                new User {
                    Id = 4,
                    UserName = "test04@unitest.com",
                    InterestPositionsName = "Ux + UI"
                }
            };
        }
    }
}