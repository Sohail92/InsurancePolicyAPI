namespace InsurancePolicyApi.Models
{
    public class AppSettings
    {
        public AzureSettings AzureSettings { get; set; }
        public int NumOfPoliciesToReturn { get; set; }
    }

    public class AzureSettings
    {
        public string AzureAccountName { get; set; }
        public string AzureAccountKey { get; set; }
        public string PoliciesTableName { get; set; }
    }
}
