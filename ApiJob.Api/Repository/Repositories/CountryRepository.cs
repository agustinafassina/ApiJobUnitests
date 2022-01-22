using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Repository.Context;

namespace ApiUnitest.ApiJob.Api.Repository.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApiDBContext _context;
        public CountryRepository(ApiDBContext context)
        {
            _context = context;
        }

        public async Task Post(Country request)
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
        public async Task Delete(Country request)
        {
            try{
                _context.Remove(request);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Task<IQueryable<Country>> GetFilter(string filter)
        {
            return Task.FromResult(_context.Countries.Where(x => x.Name == filter));
        }

        public List<Country> GetList()
        {
            return _context.Countries.ToList();
        }

        public Task<Country> GetById(int id)
        {
            return (Task<Country>)_context.Countries.Where(x => x.Id == id);
        }

        public Task Update(Country request)
        {
            _context.Update(request);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}