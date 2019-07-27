using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flies.Shared.Participants
{
    public interface IParticipantService
    {
        Task<Participant> CreateAsync(Participant participant);

        Task<IList<Participant>> GetParticipantsAsync();
        Task<Participant> GetParticipantAsync(uint id);

        Task<Participant> UpdateScoreAsync(uint id, ushort score);
        Task<Participant> UpdateNameAsync(uint id, string name);
        Task<Participant> IncreaseScoreAsync(uint id, ushort amount);
        Task<Participant> DecreaseScoreAsync(uint id, ushort amount);

        Task DeleteParticipantAsync(uint id);
    }
}
