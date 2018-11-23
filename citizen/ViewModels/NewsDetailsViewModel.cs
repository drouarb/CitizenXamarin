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

        private NewsDetailsService newsDetailsService;

        public NewsDetailsViewModel(NewsItem item)
        {
            Title = item.title;
            news = item;
            LoadNewsDetailsCommand = new Command(async () => await ExecuteLoadNewsDetailsCommand());
            newsDetailsService = new NewsDetailsService(news);
        }

        public async Task ExecuteLoadNewsDetailsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var item = await newsDetailsService.GetItemAsync(true);
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
            }
            IsBusy = false;
        }
    }
}
