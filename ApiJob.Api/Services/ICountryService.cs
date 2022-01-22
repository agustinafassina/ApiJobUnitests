using System.Collections.Generic;
using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;

namespace ApiJobUnitests.ApiJob.Api.Services
{
    public interface ICountryService
    {
        Task Post(Country request);
        Task Delete(Country request);
        Task Update(Country request);
        Task<Country> GetById(int id);
        IEnumerable<Country> ReturnList();
        List<Country> GetListFilter(string filter);
    }
}