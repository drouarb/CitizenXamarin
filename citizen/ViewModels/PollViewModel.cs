using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class PollViewModel : BaseViewModel
    {
        public Command LoadPollsCommand { get; set; }
        public ObservableCollection<PollItem> Polls { get; set; }
        public PollService PollService = new PollService();

        public PollViewModel()
        {
            Title = "Consultation";
            Polls = new ObservableCollection<PollItem>();
            LoadPollsCommand = new Command(async () => await ExecuteLoadPollsCommand());
        }

        async Task ExecuteLoadPollsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Polls.Clear();
                var items = await PollService.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Polls.Add(item);
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
