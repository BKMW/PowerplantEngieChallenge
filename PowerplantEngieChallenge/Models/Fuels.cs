using System.Text.Json.Serialization;

namespace PowerplantEngieChallenge.Models
{
    public class Fuels
    {

        [JsonPropertyName("gas(euro/MWh)")]
        public float gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public float kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public float co2 { get; set; }

        [JsonPropertyName("wind(%)")]
        public float wind { get; set; }

    }

}