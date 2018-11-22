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
        public ObservableCollection<ThreadItem> Threads { get; set; }
        public AgoraService AgoraService = new AgoraService();

        public ThreadViewModel ()
		{
            Title = "Agora";
            Threads = new ObservableCollection<ThreadItem>();
            LoadThreadCommand = new Command(async () => await ExecuteLoadThreadCommand());
        }

        async Task ExecuteLoadThreadCommand()
        {
            Console.Write("here we go");
            if (IsBusy)
                return;

            IsBusy = true;
            Console.Write("woooOOOOO");
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
            Console.Write("we lid it");
        }
    }
}