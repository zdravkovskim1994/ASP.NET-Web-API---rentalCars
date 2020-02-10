using Newtonsoft.Json;



namespace WebCarRental.Core.Models
{
    public class Customer
    {
        [JsonProperty("customerId")]
        public int CustomerID { get; set; }
        [JsonProperty("drivLicNumber")]
        public string DrvLicNumber { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }

    }
}
