using citizen.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.Views
{
    public class EventDetailModel : BaseViewModel
    {
        public EventsItem _event { get; set; }
        public Command LoadDetailCommand { get; set; }
        private EventService eventService = new EventService();

        public EventDetailModel(EventsItem @event)
        {
            _event = @event;
            Title = _event.name;
            LoadDetailCommand = new Command(async () => await ExecuteLoadEventCommand());
        }

        async Task ExecuteLoadEventCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Debug.WriteLine(_event.uuid);
                Debug.WriteLine(_event.uuid.ToString());
                _event = await eventService.GetEventDetailsAsync(_event.uuid.ToString(), true);
                OnPropertyChanged("_event");
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