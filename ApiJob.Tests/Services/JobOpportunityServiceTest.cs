using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Services;
using Moq;
using NUnit.Framework;
using ApiUnitest.ApiJob.Api.Repository.Repositories;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions;
using System.IO;

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

        [Test, Order(5)]
        public async Task GetFile_ShouldReturnStreamWriter()
        {
            var users = CreateUsers().ToList();

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersList())
                .Returns(users);

            var result = _jobOpportunityService.GetExportFile();
            var reader = new StreamReader(result.Result);
            var content = reader.ReadToEnd();

            result.Should().NotBeNull();
            result.Result.Length.Should().NotBe(0);
            content.Should().Be("test.txt");

            VerifyAll();
        }

        [Test, Order(6)]
        public async Task GetSuscriptionNow_ShouldReturnStringAndUsersTotal()
        {
            var users = CreateUsers().ToList();

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersList())
                .Returns(users);

            var result = _jobOpportunityService.GetSuscriptionNow();
            result.Should().NotBeNull();
            result.Should().ToString();
            result.Should().Be("Total users: 4");

            VerifyAll();
        }

        [Test, Order(7)]
        public async Task GetSumValueAndTotal_ShouldReturnTaskInt()
        {
            int number = 16;

            var result = _jobOpportunityService.GetSumValueAndTotal(number);
            result.Result.Should().NotBe(1500);
            result.Result.Should().NotBe(null);
            result.Result.Should().Be(1516);
            result.Should().BeOfType<Task<int>>();

            VerifyAll();
        }

        [Test, Order(8)]
        public async Task GetSumValueOneAndValueTwo_ShouldReturnInt()
        {
            int number1 = 16;
            int number2 = 10;

            var result = _jobOpportunityService.GetSumValueOneAndValueTwo(number1, number2);
            result.Should().NotBe(16);
            result.Should().NotBe(null);
            result.Should().Be(26);

            VerifyAll();
        }

        [Test, Order(9)]
        public async Task GetSumValue1Value2Value3Value4_ShouldReturnInt()
        {
            int number1 = 10;
            int number2 = 10;
            int number3 = 10;
            int number4 = 15;

            var result = _jobOpportunityService.GetSumValue1Value2Value3Value4(number1, number2, number3, number4);
            result.Should().NotBe(0);
            result.Should().Be(45);

            VerifyAll();
        }

        [Test, Order(10)]
        public async Task GetJobInternal_ShouldReturnJobOpportunitiesList()
        {
            var searchJob = new JobOpportunity {
                Name = "Ux"
            };

            var jobs = CreateJobs().ToList();

            _jobOpportunityRepositoryy.Setup(x => x.GetJobOpportunitiesList())
                .Returns(jobs);

            var result = _jobOpportunityService.GetJobInternal(searchJob);
            result.Should().BeOfType<List<JobOpportunity>>();
            result.Should().HaveCount(2);

            VerifyAll();
        }

        [Test, Order(11)]
        public async Task BodyEmail_ShouldReturnAString()
        {
            var user = new User {
                    Id = 1,
                    UserName = "test01@unitest.com",
                    InterestPositionsName = "Database Analyst"
            };

            var result = _jobOpportunityService.BodyEmailHtml(user, user.InterestPositionsName);
            result.Should().ToString();
            result.Should().Equals(user);

            VerifyAll();
        }

        [Test, Order(12)]
        public async Task CreateFile_ShouldReturnStreamWriter()
        {
            string fileName = "Test01.txt";

            var result = _jobOpportunityService.CreateFile(fileName);
            result.Should().NotBeNull();

            VerifyAll();
        }

        [Test, Order(13)]
        public async Task ReturnBool_ShouldReturnBool()
        {
            int age = 18;

            var result = _jobOpportunityService.ReturnBool(age);

            result.Should().BeTrue();

            VerifyAll();
        }

        [Test, Order(14)]
        public async Task ReturnListUser_ShouldReturnReturnListUser()
        {
            var users = CreateUsers().ToList();

            _jobOpportunityRepositoryy.Setup(x => x.GetUsersList())
                .Returns(users);

            var result = _jobOpportunityService.ReturnListUser();
            result.Should().BeOfType<List<User>>();
            result.Should().NotBeNull();

            VerifyAll();
        }

        [Test, Order(15)]
        public async Task DeleteUser_ShouldReturnOk()
        {
            var user = new User {
                    Id = 1,
                    UserName = "test01@unitest.com",
                    InterestPositionsName = "Database Analyst"
            };

            _jobOpportunityRepositoryy.Setup(x => x.DeleteUser(user))
                .Returns(Task.CompletedTask);

            await _jobOpportunityService.DeleteUser(user);

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
                    InterestPositionsName = "Ux"
                },
                new User {
                    Id = 3,
                    UserName = "test03@unitest.com",
                    InterestPositionsName = "Frontend developer"
                },
                new User {
                    Id = 4,
                    UserName = "test04@unitest.com",
                    InterestPositionsName = "Ux"
                }
            };
        }

        private IEnumerable<JobOpportunity> CreateJobs()
        {
            return new List<JobOpportunity>{
                new JobOpportunity {
                    Id = 1,
                    Name = "UI",
                    Country = "Chile",
                    Salary= 15000
                },
                new JobOpportunity {
                    Id = 2,
                    Name = "Database Analyst",
                    Country = "Argentina",
                    Salary= 15000
                },
                new JobOpportunity {
                    Id = 3,
                    Name = "Ux",
                    Country = "Brasil",
                    Salary= 170000
                },
                new JobOpportunity {
                    Id = 4,
                    Name = "Ux",
                    Country = "Chile",
                    Salary= 12000
                }
            };
        }
    }
}