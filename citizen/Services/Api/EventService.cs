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

            string rawNewsDetails = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/events/all", HttpMethod.Get);
            Console.WriteLine(rawNewsDetails);
            events = JsonConvert.DeserializeObject<List<EventsItem>>(rawNewsDetails);
            Console.WriteLine("events in thte bag " + events.Count);
            return events;
        }

        public async Task<EventsItem> GetEventDetailsAsync(string uuid, bool forceRefresh = false)
        {

            string rawNewsDetails = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/events/all", HttpMethod.Get);
            Console.WriteLine(rawNewsDetails);
            return JsonConvert.DeserializeObject<EventsItem>(rawNewsDetails);
        }
    }
}