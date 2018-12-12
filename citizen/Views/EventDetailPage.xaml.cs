using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
        EventDetailModel viewModel;

        public EventDetailPage(EventsItem _event)
        {
            InitializeComponent();
            BindingContext = viewModel = new EventDetailModel(_event);
            viewModel.LoadDetailCommand.Execute(null);

            addEvent.Clicked += async (sender, args) =>
            {
                Console.WriteLine("requesting permission");
                await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
                Console.WriteLine("checking permission");
                var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);
                Console.WriteLine("permission calendar : " + hasPermission.ToString()); 
                if (hasPermission != PermissionStatus.Granted)
                {
                    await DisplayAlert("Pas de Droits", "L'application ne dispose pas des droits pour manipuler le calendrier", "Oups");
                    return;
                }
                Calendar defaultCalendar = new Calendar();
                defaultCalendar.Color = "#7635EB";
                defaultCalendar.Name = "Citizen";
                var calendars = await CrossCalendars.Current.GetCalendarsAsync();
                Console.WriteLine("trying to find calendar in ");

                foreach (var calendar in calendars)
                {
                    Console.WriteLine("calendar is  " + calendar.CanEditEvents);
                    if (calendar.CanEditEvents)
                    {
                        Console.WriteLine("found calendar");
                        defaultCalendar = calendar;
                        break;
                    }
                }
                var calendarEvent = new CalendarEvent
                {
                    Name = viewModel._event.name,
                    Start = viewModel._event.datetime,
                    End = viewModel._event.end,
                    Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
                };
                Console.WriteLine("pls werk");
                await CrossCalendars.Current.AddOrUpdateEventAsync(defaultCalendar, calendarEvent);
                Console.WriteLine("no");
            };


        }
    }
}