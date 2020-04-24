using Microsoft.WindowsAzure.Storage.Table;

namespace InsurancePolicyApi.Models
{
    public class Policy : TableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int InsuranceGroup { get; set;}
        public string VehicleManufacturer { get; set; }
        public string VehicleModel { get; set; }
        public string PolicyCreationDate { get; set; }
        public string PolicyExpiryDate { get; set; }
    }
}
