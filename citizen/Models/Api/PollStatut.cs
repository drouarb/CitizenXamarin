using System;
using Newtonsoft.Json;

namespace citizen.Models.Api
{
    public class PollStatut
    {
        [JsonProperty("uuid")] 
        public Guid Uuid;
        [JsonProperty("result")] 
        public Guid Result;
        [JsonProperty("author")] 
        public String Author;
        [JsonProperty("pollUuid")] 
        public Guid PollUuid;
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}