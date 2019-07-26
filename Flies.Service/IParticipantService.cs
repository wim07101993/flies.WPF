using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flies.Service
{
    public interface IParticipantService
    {
        Task<IList<ParticipantPOCO>> Participants { get; }
    }
}
