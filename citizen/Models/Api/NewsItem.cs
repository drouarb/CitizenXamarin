using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace citizen.Models.Api
{
    public class NewsItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("content")]
        public string content { get; set; }
        [JsonProperty("subtitle")]
        public string subtitle { get; set; }
        //[JsonProperty("img_uuid")]
        //public Guid img_uuid { get; set; }
        [JsonProperty("created")]
        public DateTime created { get; set; }
        [JsonProperty("updated")]
        public DateTime updated { get; set; }
    }
}
