using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace citizen.Models.Api
{
    public class PostItem
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
