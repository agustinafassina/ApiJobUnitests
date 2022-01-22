using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiUnitest.ApiJob.Api.Repository.Repositories;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task Post(Country request)
        {
            try{
                await _countryRepository.Post(request);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task Delete(Country request)
        {
            return _countryRepository.Delete(request);
        }

        public async Task<Country> GetById(int id)
        {
            return await _countryRepository.GetById(id);
        }

        public List<Country> GetListFilter(string filter)
        {
            var countries = _countryRepository.GetList();
            return countries.Where(x => x.Name == filter).ToList();
        }

        public IEnumerable<Country> ReturnList()
        {
            return _countryRepository.GetList();
        }

        public Task Update(Country request)
        {
            return _countryRepository.Update(request);
        }
    }
}