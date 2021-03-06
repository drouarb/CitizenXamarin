using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using citizen.Models.Api;
using Newtonsoft.Json;

namespace citizen.Services.Api
{
    public class PollService
    {
        private List<PollItem> polls;
        
        public PollService()
        {
            polls = new List<PollItem>();
        }

        public async Task<IEnumerable<PollItem>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && polls.Count != 0)
                return polls;

            string rawPolls = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/poll", HttpMethod.Get, null);
            Console.WriteLine(rawPolls);
            polls = JsonConvert.DeserializeObject<List<PollItem>>(rawPolls);
            Console.WriteLine(polls.Count);
            polls.ForEach(poll =>
            {
                Console.WriteLine(poll.Proposition);
                Console.WriteLine(poll.Details);
            });
            return polls;
        }
    }
}