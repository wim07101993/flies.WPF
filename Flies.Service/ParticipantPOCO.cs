using Newtonsoft.Json;

namespace Flies.Service
{
    public class ParticipantPOCO
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public ushort Score { get; set; }
    }
}
