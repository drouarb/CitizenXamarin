using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    class ReportContentItem
    {
        [JsonProperty("uuid")]
        public string img_uuid;
        [JsonProperty("string")]
        public string title;
        [JsonProperty("string")]
        public string description;
        [JsonProperty("double")]
        public double lat;
        [JsonProperty("double")]
        public double lon;
    }
}
