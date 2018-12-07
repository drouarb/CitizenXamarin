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

        public NewsDetailsViewModel()
        {
            Title = "news";
            LoadNewsDetailsCommand = new Command<Guid>(async (uuid) => await ExecuteLoadNewsDetailsCommand(uuid));
            newsDetailsService = new NewsDetailsService(news);
        }

        async Task ExecuteLoadNewsDetailsCommand(Guid uuid)
        {
            Console.WriteLine("is busy");
            if (IsBusy)
                return;
            Console.WriteLine("isntbusy");
            IsBusy = true;

            try
            {
                Console.WriteLine("news");
                var item = await newsDetailsService.GetItemAsync(uuid.ToString(), true);
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
