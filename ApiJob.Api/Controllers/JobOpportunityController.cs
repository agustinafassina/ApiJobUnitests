using System.Threading.Tasks;
using ApiJobUnitests.ApiJob.Api.Models;
using ApiJobUnitests.ApiJob.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiJobUnitests.ApiJob.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobOpportunityController : ControllerBase
    {
        private readonly IJobOpportunityService _jobOpportunityService;

        public JobOpportunityController(IJobOpportunityService jobOpportunityService)
        {
            _jobOpportunityService = jobOpportunityService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobOpportunity request)
        {
            await _jobOpportunityService.Post(request);
            return Ok();
        }

        [HttpGet]
        public object Get([FromQuery] JobOpportunity filters)
        {
            return _jobOpportunityService.JobSearch(filters);
        }

        [HttpPost("suscriptions")]
        public async Task<IActionResult> PostSuscription([FromBody] User request)
        {
            await _jobOpportunityService.PostSuscription(request);
            return Ok();
        }

        [HttpGet("suscriptions/user")]
        public object GetSuscriptionUser()
        {
            return _jobOpportunityService.GetSuscriptionUser();
        }
    }
}
