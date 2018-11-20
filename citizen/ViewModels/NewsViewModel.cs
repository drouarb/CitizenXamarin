using citizen.Models.Api;
using citizen.Services.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    class NewsViewModel : BaseViewModel
    {
        public Command LoadNewsCommand { get; set; }
        public ObservableCollection<NewsItem> News { get; set; }
        public NewsService newsService = new NewsService();

        public NewsViewModel()
        {
            Title = "Actualité";
            News = new ObservableCollection<NewsItem>();
            LoadNewsCommand = new Command(async () => await ExecuteLoadNewsCommand());
        }

        async Task ExecuteLoadNewsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                News.Clear();
                var news = await newsService.GetNewsAsync(true);
                foreach (var item in news)
                {
                    News.Add(item);
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
