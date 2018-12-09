using Newtonsoft.Json;
using System;

namespace citizen.Views
{
    public class EventsItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("datetime")]
        public DateTime datetime { get; set; }
        [JsonProperty("end")]
        public DateTime end { get; set; }
        [JsonProperty("img_uuid")]
        public string img_uuid { get; set; } //dont change or it will crash if there is no img
        [JsonProperty("created")]
        public DateTime created { get; set; }
        [JsonProperty("updated")]
        public DateTime updated { get; set; }
    }
}