using System.Collections.Generic;
using System.Threading.Tasks;
using InsurancePolicyApi.Models;

namespace InsurancePolicyApi.Interfaces
{
    public interface IRetrievePolicies
    {
        Task<IEnumerable<Policy>> GetAllPoliciesAsync();
    }
}
