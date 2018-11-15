using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models;
using citizen.Services;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class PollViewModel : BaseViewModel
    {
        public Command LoadPollsCommand { get; set; }
        public ObservableCollection<PollItem> Polls { get; set; }
        public IDataStore<PollItem> PollStore => DependencyService.Get<IDataStore<PollItem>>();

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
                var items = await PollStore.GetItemsAsync(true);
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
