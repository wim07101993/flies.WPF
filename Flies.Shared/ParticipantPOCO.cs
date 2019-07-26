using Newtonsoft.Json;

namespace Flies.Shared
{
    internal class ParticipantPOCO
    {
        public ParticipantPOCO()
        {

        }

        public ParticipantPOCO(Participant participant)
        {
            Id = participant.Id;
            Name = participant.Name;
            Score = participant.Score;
        }


        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public ushort Score { get; set; }


        public Participant ToParticipant() => new Participant
        {
            Id = Id,
            Name = Name,
            Score = Score
        };
    }
}
