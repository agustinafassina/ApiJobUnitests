using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Services;
using Moq;
using NUnit.Framework;
using ApiUnitest.ApiJob.Api.Repository.Repositories;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;

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

            var users = CreateUsers().AsQueryable();

            _jobOpportunityRepositoryy.Setup(x => x.PostJobs(It.IsAny<JobOpportunity>()))
                .Returns(Task.CompletedTask);

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersFilter(It.Is<string>(p => p.Equals(job.Name))))
                .Returns(Task.FromResult<IQueryable<User>>(users));

            await _jobOpportunityService.Post(job);

            VerifyAll();
        }

        [Test, Order(2)]
        public async Task GetSuscription_ShouldReturnUserList()
        {
            var users = CreateUsers().ToList();

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersList())
                .Returns(users);

            var result = _jobOpportunityService.GetSuscriptionUser();
            result.Should().GetType().Equals("List");
            result.Should().HaveCount(4);

            VerifyAll();
        }

        [Test, Order(3)]
        public async Task PostSuscription_ShouldReturnOk()
        {
            var user = new User {
                    Id = 1,
                    UserName = "test01@unitest.com",
                    InterestPositionsName = "Database Analyst"
            };

            _jobOpportunityRepositoryy.Setup(x => x.PostUser(user))
                .Returns(Task.CompletedTask);

            await _jobOpportunityService.PostSuscription(user);

            VerifyAll();
        }

        [Test, Order(4)]
        public async Task GetUserById_ShouldReturnOneUser()
        {
            var user = new User {
                    Id = 1,
                    UserName = "test01@unitest.com",
                    InterestPositionsName = "Database Analyst"
            };

            _jobOpportunityRepositoryy.Setup(x => x.GetUserById(user.Id))
                .Returns(Task.FromResult(user));

            var result = _jobOpportunityService.GetUserById(user.Id);
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<User>>();

            VerifyAll();
        }

        private void VerifyAll()
        {
            _jobOpportunityRepositoryy.VerifyAll();
        }

        private IEnumerable<User> CreateUsers()
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