using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InsurancePolicyApi.Interfaces;
using InsurancePolicyApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InsurancePolicyApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IRetrievePolicies _policyRetrievalLogic;
        private readonly IStorePolicies _policyStoringLogic;
        private readonly AppSettings _settings;

        public PolicyController(ILogger<PolicyController> logger, IOptions<AppSettings> settings, 
            IRetrievePolicies policyRetrievalLogic, IStorePolicies policyStoringLogic )
        {
            _logger = logger;
            _settings = settings.Value;
            _policyRetrievalLogic = policyRetrievalLogic;
            _policyStoringLogic = policyStoringLogic;
        }
        
        [HttpGet]
        [Route("AllPolicies")]
        public async Task<IEnumerable<Policy>> AllPolicies()
        {
            var result = await _policyRetrievalLogic.GetAllPoliciesAsync();
            return result;
        }

        [HttpGet]
        [Route("GetBMWPolicies")]
        public async Task<IEnumerable<Policy>> GetBmwPolicies()
        {
            var result = await _policyRetrievalLogic.GetAllPoliciesAsync();
            return result.Where(a=>a.VehicleManufacturer == "BMW").Take(_settings.NumOfPoliciesToReturn);
        }

        [HttpPost]
        [Route("AddPolicy")]
        public string AddPolicy([FromBody] Policy policy) // <-- Other options available such as FromQuery, FromRoute, FromHeader, FromForm
        {
            string result = string.Empty;
            try
            {
                result = _policyStoringLogic.AddPolicy(policy);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Error: {ex} | Policy: {JsonSerializer.Serialize(policy)}"); // <-- here we use .NET Cores built in logging and the new JsonSerializer
            }
            return result;
        }


    }
}