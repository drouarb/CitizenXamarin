using Newtonsoft.Json;

namespace citizen.Models.Api
{
    public class PollResult
    {
        [JsonProperty("result")] 
        public string ResultUuid;
    }
}