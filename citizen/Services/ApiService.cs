using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace citizen.Services
{
    public class ApiService
    {
        private HttpClient _httpClient;
        private AuthenticationResponse _authenticationResponse = null;
        
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.MaxResponseContentBufferSize = 256000;
        }

        public bool IsAuthenticated()
        {
            return _authenticationResponse != null;
        }

        public async Task<bool> Authenticate(string Username, string Password)
        {
            var uri = new Uri (string.Format ("https://oauth.citizen.navispeed.eu/oauth/token", string.Empty));
            
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("grant_type", "password")); 
            values.Add(new KeyValuePair<string, string>("username", Username)); 
            values.Add(new KeyValuePair<string, string>("password", Password));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y2l0aXplbjpzZWNyZXQ=");
            HttpResponseMessage resp = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(values));
            if (!resp.IsSuccessStatusCode)
                return false;

            _authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await resp.Content.ReadAsStringAsync());
            return true;
        }

        public async Task<string> ApiRequest(string url, HttpMethod verb)
        {
            //TODO Implement Authentication Check
            //Build the Request
            //TODO Implement custom body
            HttpRequestMessage req = new HttpRequestMessage(verb, url);
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationResponse.accessToken);
            HttpResponseMessage resp = await _httpClient.SendAsync(req);

           //TODO Check Result
            return await resp.Content.ReadAsStringAsync();
        }

        public async Task<List<ThreadItem>> GetThreads()
        {
            var uri = new Uri(string.Format("https://citizen.navispeed.eu/api/threads", string.Empty));


            Log.Warning("GetThreads", "CALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Token");

            Log.Warning("GetThreads", "HEADERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            Log.Warning("GetThreads", response.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Log.Warning("GetThreads", content);
                return JsonConvert.DeserializeObject<List<ThreadItem>>(content);
            }

            // return response.StatusCode;
            return null;
        }
    }
}