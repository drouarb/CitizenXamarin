using citizen.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace citizen.Services.Api
{

    class NewsDetailsService
    {
        private NewsItem news;

        public NewsDetailsService(NewsItem item) { 
            this.news = item;
        }
    }
}
