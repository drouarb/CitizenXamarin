using System;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    public class PollChoice
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("pollUuid")]
        public Guid PollUuid { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}