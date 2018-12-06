using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using citizen.Models.Api;
using Newtonsoft.Json;

namespace citizen.Services
{
    class NotificationService
    {
        private List<NotificationItem> notifications;

        public async Task<IEnumerable<NotificationItem>> GetNotificationsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && notifications.Count != 0)
                return notifications;

            string rawThreads = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/notification/unread", HttpMethod.Get, null);
            Console.WriteLine("raw threads:" + rawThreads);
            notifications = JsonConvert.DeserializeObject<List<NotificationItem>>(rawThreads);
            Console.WriteLine("threads count" + notifications.Count);
            notifications.ForEach(thread =>
            {
                Console.WriteLine(thread.Title);
                Console.WriteLine(thread.Created);
            });
            return notifications;
        }
    }
}
