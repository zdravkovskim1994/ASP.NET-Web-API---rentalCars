using Newtonsoft.Json;
using System;


namespace WebCarRental.Core.Models
{
    public class RentalOrder
    {
        [JsonProperty("rentalOrderID")]
        public int RentalOrderID { get; set; }
        [JsonProperty("customerID")]
        public int CustomerID { get; set; }
        [JsonProperty("carID")]
        public int CarID { get; set; }
        [JsonProperty("rentStartDate")]
        public DateTime RentStartDate { get; set; }
        [JsonProperty("rentEndDate")]
        public DateTime RentEndDate { get; set; }
            

    }
}
