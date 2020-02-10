using Newtonsoft.Json;


namespace WebCarRental.Core.Models
{
    public class Car
    {
        [JsonProperty("carId")]
        public int CarId { get; set; }

        [JsonProperty("tagNumber")]
        public string TagNumber { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("carYear")]
        public int CarYear { get; set; }

        [JsonProperty("airConditioner")]
        public byte AirConditioner { get; set; }

        [JsonProperty("daily")]
        public int Daily { get; set; }

        [JsonProperty("monthly")]
        public int Monthly { get; set; }
    }
}
