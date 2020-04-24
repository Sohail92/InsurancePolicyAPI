using InsurancePolicyApi.Interfaces;
using InsurancePolicyApi.Models;

namespace InsurancePolicyApi.Logic
{
    public class PolicyStoringLogic : IStorePolicies
    {
        public string AddPolicy(Policy policy)
        {
            if (!string.IsNullOrEmpty(policy.PolicyCreationDate))
            {
                return "Policy added";
            }

            return "Policy cannot be added until it has a valid creation date";
        }
    }
}
