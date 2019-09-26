using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flies.Shared.Participants
{
    public class ParticipantService : IParticipantService
    {
        #region FIELDS

        private readonly ParticipantServiceSettings _settings;

        #endregion FIELDS


        #region CONSTRUCTOR

        public ParticipantService(ParticipantServiceSettings settings)
        {
            _settings = settings;
        }

        #endregion CONSTRUCTOR


        #region PROPERTIES

        public string Url => $"http://{_settings.IpAddress}:{_settings.PortNumber}/api/participants";

        #endregion PROPERTIES


        #region METHODS

        public async Task<Participant> CreateAsync(Participant participant)
        {
            var poco = new ParticipantDTO(participant);
            var response = await Url.PostJsonAsync(poco);
            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> GetParticipantAsync(uint id)
        {
            var poco = await Url
                .AppendPathSegment(id)
                .GetJsonAsync<ParticipantDTO>();

            return (Participant)poco;
        }

        public async Task<IList<Participant>> GetParticipantsAsync()
        {
            var pocos = await Url.GetJsonAsync<List<ParticipantDTO>>();
            return pocos
                .Select(x => (Participant)x)
                .ToList();
        }

        public async Task<Participant> IncreaseScoreAsync(uint id, ushort amount)
        {
            var response = await Url
                .AppendPathSegment(id)
                .AppendPathSegment("increaseScore")
                .SetQueryParams(new { amount })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> DecreaseScoreAsync(uint id, ushort amount)
        {
            var response = await Url
                .AppendPathSegment(id)
                .AppendPathSegment("decreaseScore")
                .SetQueryParams(new { amount })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> UpdateNameAsync(uint id, string newName)
        {
            var response = await Url
                .AppendPathSegments(id, "name")
                .SetQueryParams(new { newName })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> UpdateScoreAsync(uint id, ushort score)
        {
            var response = await Url
               .AppendPathSegments(id,"score")
               .SetQueryParams(new { score })
               .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task DeleteParticipantAsync(uint id)
        {
            var response = await Url
               .AppendPathSegment(id)
               .DeleteAsync();
        }

        private async Task<Participant> GetParticipantFromResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return (Participant)JsonConvert.DeserializeObject<ParticipantDTO>(content);
        }

        #endregion METHDOS
    }
}
