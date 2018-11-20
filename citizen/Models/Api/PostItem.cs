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
        [JsonProperty("thread_uuid")]
        public string ThreadUuid { get; set; }
        [JsonProperty("posted")]
        public DateTime Created { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
