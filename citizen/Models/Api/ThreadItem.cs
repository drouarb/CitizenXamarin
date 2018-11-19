using System;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    public class ThreadItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
    }
}
