using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace citizen.Views
{
    internal class EventService
    {
        List<EventsItem> events;

        public async Task<List<EventsItem>> GetEventsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false)
                return events;

            string rawNewsDetails = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/events/all", HttpMethod.Get);
            Console.WriteLine(rawNewsDetails);
            events = JsonConvert.DeserializeObject<List<EventsItem>>(rawNewsDetails);
            return events;
        }
    }
}