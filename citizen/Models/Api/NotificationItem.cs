using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    class NotificationItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("userUuid")]
        public Guid UserUuid { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("viewed")]
        public Boolean Viewed { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
