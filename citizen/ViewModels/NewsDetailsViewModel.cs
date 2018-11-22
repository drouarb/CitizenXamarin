using citizen.Models.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace citizen.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        public NewsItem news { get; set; }

        public NewsDetailsViewModel(NewsItem item)
        {
            news = item;
            Title = item.title;
        }
    }
}
