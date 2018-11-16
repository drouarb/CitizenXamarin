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
        public ThreadStore ThreadStore { get; set; }

        public ForumTopicViewModel ()
		{
            Title = "Agora";
            Threads = new ObservableCollection<ThreadItem>();
            ThreadStore = new ThreadStore();
            LoadTopicCommand = new Command(async () => await ExecuteLoadThreadCommand());
            LoadTopicCommand.Execute(null);
        }

        async Task ExecuteLoadThreadCommand()
        {
            Xamarin.Forms.Internals.Log.Warning("CAT","##################################### TEST");
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