using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using citizen.Models.Api;

namespace citizen.Services.Api
{
    public class PollDetailsService
    {
        private PollItem poll;
        private List<PollChoice> choices;
        
        public PollDetailsService(PollItem poll)
        {
            this.poll = poll;
            choices = new List<PollChoice>();
        }

        public async Task<IEnumerable<PollChoice>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && choices.Count != 0)
                return await Task.FromResult(choices);

            string rawChoices = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/poll/poll/" + poll.Uuid + "/choices", HttpMethod.Get, null);
            return await Task.FromResult(choices);
        }
    }
}