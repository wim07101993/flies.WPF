using Newtonsoft.Json;

namespace Flies.Shared.Participants
{
    internal class ParticipantDTO
    {
        public ParticipantDTO()
        {

        }

        public ParticipantDTO(Participant participant)
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


        public static explicit operator Participant(ParticipantDTO dto) => new Participant
        {
            Id = dto.Id,
            Name = dto.Name,
            Score = dto.Score
        };


        public static explicit operator ParticipantDTO(Participant participant) => new ParticipantDTO
        {
            Id = participant.Id,
            Name = participant.Name,
            Score = participant.Score
        };
    }
}
