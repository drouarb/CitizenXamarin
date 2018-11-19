using System;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    public class PollItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("proposition")]
        public string Proposition { get; set; }
        [JsonProperty("details")]
        public string Details { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        [JsonProperty("published")]
        public bool Published { get; set; }
    }
}