using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using citizen.Models;
using citizen.Models.Api;
using citizen.ViewModels;
using Newtonsoft.Json;

namespace citizen.Services.Api
{
    class ReportService
    {
        public async Task<String> ReportPostAsync(ReportContentItem content)
        {
            string rawValue = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/reports", HttpMethod.Post, content.ToString());
            Console.WriteLine("Report Post: " + rawValue);
            return rawValue;
        }
    }
}
