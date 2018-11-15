using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models;
using citizen.Services;
using Xamarin.Forms;

namespace citizen.ViewModels
{
	public class ForumTopicViewModel : BaseViewModel
    {
        public Command LoadTopicCommand { get; set; }
        public ObservableCollection<ThreadItem> Threads { get; set; }
        public IDataStore<ThreadItem> ThreadStore => DependencyService.Get<IDataStore<ThreadItem>>();

        public ForumTopicViewModel ()
		{
            Title = "Agora";
            Threads = new ObservableCollection<ThreadItem>();
            LoadTopicCommand = new Command(async () => await ExecuteLoadThreadCommand());
        }

        async Task ExecuteLoadThreadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Threads.Clear();
                var items = await ThreadStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Threads.Add(item);
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