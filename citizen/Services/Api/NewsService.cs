using citizen.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace citizen.Services.Api
{
    class NewsService
    {
        private List<NewsItem> news;

        public NewsService()
        {
            news = new List<NewsItem>();
        }

        public async Task<IEnumerable<NewsItem>> GetNewsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && news.Count != 0)
                return await Task.FromResult(news);
            Console.WriteLine("CALLLLLLLLLLLLLLLLLLLLLLLLL NEWS");
            string rawNews = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/news", HttpMethod.Get);
            Console.WriteLine("raw news:" + rawNews);
            news = JsonConvert.DeserializeObject<List<NewsItem>>(rawNews);
            Console.WriteLine("news count" + news.Count);
            news.ForEach(news =>
            {
                Console.WriteLine(news.title);
                Console.WriteLine(news.subtitle);
            });
            return await Task.FromResult(news);            
        }
    }
}
