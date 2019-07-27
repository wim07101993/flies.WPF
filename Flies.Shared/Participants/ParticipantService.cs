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

        public async Task<Participant> Create(Participant participant)
        {
            var poco = new ParticipantPOCO(participant);
            var response = await Url.PostJsonAsync(poco);
            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> GetParticipant(uint id)
        {
            var poco = await Url
                .AppendPathSegment(id)
                .GetJsonAsync<ParticipantPOCO>();

            return poco.ToParticipant();
        }

        public async Task<IList<Participant>> GetParticipants()
        {
            var pocos = await Url.GetJsonAsync<List<ParticipantPOCO>>();
            return pocos
                .Select(x => x.ToParticipant())
                .ToList();
        }

        public async Task<Participant> IncreaseScore(uint id, ushort amount)
        {
            var response = await Url
                .AppendPathSegment(id)
                .AppendPathSegment("increaseScore")
                .SetQueryParams(new { amount })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> DecreaseScore(uint id, ushort amount)
        {
            var response = await Url
                .AppendPathSegment(id)
                .AppendPathSegment("decreaseScore")
                .SetQueryParams(new { amount })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> UpdateName(uint id, string name)
        {
            var response = await Url
                .AppendPathSegment(id)
                .SetQueryParams(new { name })
                .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task<Participant> UpdateScore(uint id, ushort score)
        {
            var response = await Url
               .AppendPathSegment(id)
               .SetQueryParams(new { score })
               .PutStringAsync("");

            return await GetParticipantFromResponse(response);
        }

        public async Task DeleteScore(uint id)
        {
            var response = await Url
               .AppendPathSegment(id)
               .DeleteAsync();
        }

        private async Task<Participant> GetParticipantFromResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ParticipantPOCO>(content)
                .ToParticipant();
        }

        #endregion METHDOS
    }
}
