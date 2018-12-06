using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using citizen.Models.Api;
using citizen.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
using System.IO;

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

        public async Task<bool> RefreshToken()
        {
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("refresh_token", _authenticationResponse.refreshToken));
            values.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y2l0aXplbjpzZWNyZXQ=");
            HttpResponseMessage resp = await _httpClient.PostAsync("https://oauth.citizen.navispeed.eu/oauth/token", new FormUrlEncodedContent(values));
            if (!resp.IsSuccessStatusCode)
                return false;

            _authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await resp.Content.ReadAsStringAsync());
            return true;
        }

        public async Task<string> ApiRequest(string url, HttpMethod verb, String JSONBody = null)
        {
            //Build the Request
            //TODO Implement custom body
            HttpRequestMessage req = new HttpRequestMessage(verb, url);
            if (JSONBody != null)
                req.Content = new StringContent(JSONBody, Encoding.UTF8, "application/json");
            
            /* testing refresh token
             * to be removed in final release
             */
            Console.WriteLine("access token: " + _authenticationResponse.accessToken);
            Console.WriteLine("does refresh token work ? " + await RefreshToken());
            Console.WriteLine("access token: " + _authenticationResponse.accessToken);

            //req.Content = new StringContent("test");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationResponse.accessToken);
            HttpResponseMessage resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                if (await RefreshToken())
                    return ApiRequest(url, verb, JSONBody).Result;
           //TODO Check Result
            return await resp.Content.ReadAsStringAsync();
        }

        public async Task<string> UploadFile(string MIME, Stream fileStream)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationResponse.accessToken);
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent content = new StreamContent(fileStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("MIME");
            form.Add(content);
            var response = await client.PostAsync("https://citizen.navispeed.eu/api/common/upload/file", form);
            return await response.Content.ReadAsStringAsync();
        }
    }
}