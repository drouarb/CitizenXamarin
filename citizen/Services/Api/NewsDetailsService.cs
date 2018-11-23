using citizen.Models.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace citizen.Services.Api
{

    class NewsDetailsService
    {
        private NewsItem news;

        public NewsDetailsService(NewsItem item) { 
            this.news = item;
        }

        public async Task<NewsItem> GetItemAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false)
                return news;

            Console.WriteLine("CALLLLLLLLLLLLLLLLLLLLL NEWS DETAILS");
            string rawNewsDetails = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/news/" + news.Uuid, HttpMethod.Get);
            Console.WriteLine(rawNewsDetails);
            news = JsonConvert.DeserializeObject<NewsItem>(rawNewsDetails);
            Console.WriteLine("CONTENT: " + news.content);
            return news;
        }
    }
}
