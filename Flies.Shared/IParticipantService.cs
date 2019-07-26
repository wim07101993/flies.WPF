using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flies.Shared
{
    public interface IParticipantService
    {
        Task<Participant> Create(Participant participant);

        Task<IList<Participant>> GetParticipants();
        Task<Participant> GetParticipant(uint id);

        Task<Participant> UpdateScore(uint id, ushort score);
        Task<Participant> UpdateName(uint id, string name);
        Task<Participant> IncreaseScore(uint id, ushort amount);
        Task<Participant> DecreaseScore(uint id, ushort amount);

        Task DeleteScore(uint id);
    }
}
