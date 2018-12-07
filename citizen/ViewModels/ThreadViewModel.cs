using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using Xamarin.Forms;

namespace citizen.ViewModels
{
	public class ThreadViewModel : BaseViewModel
    {
        public Command LoadThreadCommand { get; set; }
        public Command CreateThreadCommand { get; set; }
        public ObservableCollection<ThreadItem> Threads { get; set; }
        public AgoraService AgoraService = new AgoraService();

        public ThreadViewModel ()
		{
            Title = "Agora";
            Threads = new ObservableCollection<ThreadItem>();
            LoadThreadCommand = new Command(async () => await ExecuteLoadThreadCommand());
            CreateThreadCommand = new Command<String>(async (threadName) => await ExecuteCreateThreadCommand(threadName));
        }

        async Task ExecuteCreateThreadCommand(String threadName)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try { await AgoraService.CreateThreadAsync(threadName); }
            finally { IsBusy = false; }
            }

        async Task ExecuteLoadThreadCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Threads.Clear();
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