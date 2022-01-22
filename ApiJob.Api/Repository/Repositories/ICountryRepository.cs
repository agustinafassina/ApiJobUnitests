using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiUnitest.ApiJob.Api.Repository.Repositories
{
    public interface ICountryRepository
    {
        Task Post (Country request);
        Task Delete(Country request);
        Task<IQueryable<Country>> GetFilter(string filter);
        List<Country> GetList();
        Task<Country> GetById(int id);
        Task Update(Country request);
    }
}