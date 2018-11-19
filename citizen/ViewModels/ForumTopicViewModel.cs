using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using Xamarin.Forms;

namespace citizen.ViewModels
{
	public class ForumTopicViewModel : BaseViewModel
    {
        public Command LoadTopicCommand { get; set; }
        public ObservableCollection<ThreadItem> Threads { get; set; }
        public AgoraService AgoraService = new AgoraService();

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
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAaa");
                var threads = await AgoraService.GetThreadsAsync(true);
                foreach (var item in threads)
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