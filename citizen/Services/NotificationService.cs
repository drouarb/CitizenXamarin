using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using citizen.Models.Api;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace citizen.Services
{
    public class NotificationService
    {
        private Random random = new Random();
        private List<NotificationItem> notifications;

        public async Task<IEnumerable<NotificationItem>> GetNotificationsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && notifications.Count != 0)
                return notifications;

            string rawNotifications =
                await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/notification/unread", HttpMethod.Get);
            Console.WriteLine("Notifs:" + rawNotifications);
            notifications = JsonConvert.DeserializeObject<List<NotificationItem>>(rawNotifications);
            Console.WriteLine("Notifs count" + notifications.Count);
            return notifications;
        }

        public async Task FetchNotifications()
        {
            if (App.ApiService.IsAuthenticated() == false)
                return;

            await GetNotificationsAsync(true);
            foreach (var notification in notifications)
            {
                if (!Application.Current.Properties.ContainsKey("notification-" + notification.Uuid))
                {
                    Console.WriteLine(notification.Title);
                    Application.Current.Properties["notification-" + notification.Uuid] = notification;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var notificationService = DependencyService.Get<ILocalNotificationService>();
                        notificationService.Show(new LocalNotification
                        {
                            NotificationId = random.Next(0, 1000000000),
                            Title = notification.Title,
                            Description = notification.Url,
                            ReturningData = notification.Url,
                            NotifyTime = DateTime.Now.AddSeconds(1)
                        });
                    });
                }
            }
            await Application.Current.SavePropertiesAsync();
        }
    }
}