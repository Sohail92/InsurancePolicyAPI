using InsurancePolicyApi.Models;

namespace InsurancePolicyApi.Interfaces
{
    public interface IStorePolicies
    {
        string AddPolicy(Policy policy);
    }
}
