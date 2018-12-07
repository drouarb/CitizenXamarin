using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    class ReportContentItem
    {
        [JsonProperty("img_uuid")]
        public string img_uuid;
        [JsonProperty("title")]
        public string title;
        [JsonProperty("description")]
        public string description;
        [JsonProperty("lat")]
        public double lat;
        [JsonProperty("lon")]
        public double lon;
    }
}
