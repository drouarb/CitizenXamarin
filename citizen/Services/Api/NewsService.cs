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
            string rawNews = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/news", HttpMethod.Get);
            news = JsonConvert.DeserializeObject<List<NewsItem>>(rawNews);
            return await Task.FromResult(news);            
        }
    }
}
