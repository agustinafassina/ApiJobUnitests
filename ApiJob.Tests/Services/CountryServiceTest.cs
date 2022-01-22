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
    public class CountryServiceTest
    {
        private readonly ICountryService _countryService;
        private readonly Mock<ICountryRepository> _countryRepository;
        public CountryServiceTest()
        {
            _countryRepository = new Mock<ICountryRepository>(MockBehavior.Strict);
            _countryService = new CountryService(_countryRepository.Object);
        }

        [Test, Order(1)]
        public async Task InsertCountry_ShouldReturnTaskOk()
        {
            var country = new Country {
                Id = 1,
                Name = "Argentina",
                MaxSalary = 1500
            };

            _countryRepository.Setup(x => x.Post(It.IsAny<Country>()))
                .Returns(Task.CompletedTask);

            await _countryService.Post(country);

            VerifyAll();
        }

        [Test, Order(2)]
        public async Task DeleteCountry_ShouldReturnOk()
        {
            var country = new Country {
                Id = 1,
                Name = "Chile",
                MaxSalary = 15000
            };

            _countryRepository.Setup(x => x.Delete(country))
                .Returns(Task.CompletedTask);

            await _countryService.Delete(country);

            VerifyAll();
        }

        [Test, Order(3)]
        public async Task UpdateCountry_ShouldReturnOk()
        {
            var country = new Country {
                Id = 1,
                Name = "Chile",
                MaxSalary = 15000
            };

            _countryRepository.Setup(x => x.Update(country))
                .Returns(Task.CompletedTask);

            await _countryService.Update(country);

            VerifyAll();
        }

        [Test, Order(4)]
        public async Task GetCountryById_ShouldReturnOneCountry()
        {
            var country = new Country {
                Id = 1,
                Name = "Paraguay",
                MaxSalary = 15000
            };

            _countryRepository.Setup(x => x.GetById(country.Id))
                .Returns(Task.FromResult(country));

            var result = _countryService.GetById(country.Id);
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Country>>();

            VerifyAll();
        }

        [Test, Order(5)]
        public async Task ReturnListUser_ShouldReturnReturnListUser()
        {
            var users = CreateCountries().ToList();

            _countryRepository.Setup(x => x.GetList())
                .Returns(users);

            var result = _countryService.ReturnList();
            result.Should().BeOfType<List<Country>>();
            result.Should().NotBeNull();

            VerifyAll();
        }

        private void VerifyAll()
        {
            _countryRepository.VerifyAll();
        }

        private IEnumerable<Country> CreateCountries()
        {
            return new List<Country>{
                new Country {
                    Id = 1,
                    Name = "Italia",
                    MaxSalary = 1500
                },
                new Country {
                    Id = 2,
                    Name = "España",
                    MaxSalary = 1600
                },
                new Country {
                    Id = 3,
                    Name = "Canada",
                    MaxSalary = 19000
                },
                new Country {
                    Id = 4,
                    Name = "EEUUs",
                    MaxSalary = 1500
                }
            };
        }
    }
}