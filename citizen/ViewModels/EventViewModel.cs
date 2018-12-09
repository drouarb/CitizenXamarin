using citizen.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.Views
{
    internal class EventViewModel : BaseViewModel
    {
        public Command LoadEventCommand { get; set; }
        public ObservableCollection<EventsItem> Events { get; set; }
        private EventService eventService = new EventService();

        public EventViewModel()
        {
            Title = "Event";
            Events = new ObservableCollection<EventsItem>();
            LoadEventCommand = new Command(async () => await ExecuteLoadEventCommand());
        }

        async Task ExecuteLoadEventCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Events.Clear();
                var events = await eventService.GetEventsAsync(true);
                foreach (var item in events)
                {
                    Events.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}