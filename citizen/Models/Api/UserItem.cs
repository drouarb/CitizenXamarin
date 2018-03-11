using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace citizen.Models.Api
{
    class UserItem
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        [JsonProperty("userUUID")]
        public string userId { get; set; }
        [JsonProperty("profilePictureUUID")]
        public Guid profilePictureUUID { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("pseudo")]
        public string pseudo { get; set; }
        [JsonProperty("bio")]
        public string bio { get; set; }
    }
}
