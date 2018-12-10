using citizen.Models.Api;
using citizen.Services.Api;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        public NewsItem news { get; set; }

        public Command LoadNewsDetailsCommand { get; set; }

        private NewsDetailsService NewsDetailsService;

        public NewsDetailsViewModel(Guid newsUuid, String title)
        {
            Title = title;
            NewsDetailsService = new NewsDetailsService(newsUuid);
            LoadNewsDetailsCommand = new Command(async () => ExecuteLoadNewsDetailsCommand());
        }

        async Task ExecuteLoadNewsDetailsCommand()
        {
            Console.WriteLine("is busy");
            if (IsBusy)
                return;
            Console.WriteLine("isntbusy");
            IsBusy = true;

            try
            {
                Console.WriteLine("news");
                var item = await NewsDetailsService.GetItemAsync(true);
                news = item;
                Console.WriteLine("ITEMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM: " + news.content);

            } catch (Exception e)
            {
                Console.WriteLine("EXCEPTION: " + e);
                Debug.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
                LoadNewsDetailsCommand.ChangeCanExecute();
            }
        }
    }
}
