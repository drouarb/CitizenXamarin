using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using citizen.Models.Api;
using Newtonsoft.Json;

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

        public PollDetailsService(Guid uuid)
        {
            poll = new PollItem();
            poll.Uuid = uuid;
            choices = new List<PollChoice>();
        }

        public async Task<PollItem> GetPoll()
        {
            string rawPoll = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/poll/poll/" + poll.Uuid,
                HttpMethod.Get);
            Console.WriteLine(rawPoll);

            if (String.IsNullOrEmpty(rawPoll))
                return null;
            poll = JsonConvert.DeserializeObject<PollItem>(rawPoll);
            Console.WriteLine("Le poll c'est " + poll.Proposition);
            return poll;
        }

        public async Task<PollStatut> GetResultAsync()
        {
            string rawStatus = await App.ApiService.ApiRequest(
                "https://citizen.navispeed.eu/api/poll/poll/" + poll.Uuid + "/statut", HttpMethod.Get, null);

            if (String.IsNullOrEmpty(rawStatus))
                return null;

            return JsonConvert.DeserializeObject<PollStatut>(rawStatus);
        }

        public async Task<IEnumerable<PollChoice>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && choices.Count != 0)
                return choices;

            string rawChoices = await App.ApiService.ApiRequest(
                "https://citizen.navispeed.eu/api/poll/poll/" + poll.Uuid + "/choices", HttpMethod.Get, null);
            Console.WriteLine(rawChoices);
            choices = JsonConvert.DeserializeObject<List<PollChoice>>(rawChoices);
            Console.WriteLine(choices.Count);
            PollStatut statut = await GetResultAsync();
            if (statut != null)
            {
                choices.ForEach(choice =>
                {
                    if (choice.Uuid == statut.Result)
                        choice.Selected = true;
                });
            }

            return choices;
        }

        public async Task Vote(PollChoice choice)
        {
            PollResult pollResult = new PollResult();
            pollResult.ResultUuid = choice.Uuid.ToString();
            string body = JsonConvert.SerializeObject(pollResult);

            Console.WriteLine(body);
            string resp = await App.ApiService.ApiRequest(
                "https://citizen.navispeed.eu/api/poll/poll/" + choice.PollUuid + "/results", HttpMethod.Post, body);
            Console.WriteLine(resp);
        }
    }
}