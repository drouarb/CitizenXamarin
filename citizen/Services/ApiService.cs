using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using citizen.Models.Api;
using citizen.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace citizen.Services
{
    public class ApiService
    {
        private HttpClient _httpClient;
        
        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<bool> IsAuthenticated()
        {
            Console.WriteLine("Has access token ? " + Application.Current.Properties.ContainsKey("accessToken"));
            if (!Application.Current.Properties.ContainsKey("accessToken"))
                return false;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["accessToken"].ToString());
            HttpResponseMessage resp = await _httpClient.GetAsync("https://oauth.citizen.navispeed.eu/token/test");
            
            Console.WriteLine("API AccessToken check: " + resp.StatusCode);
            if (resp.StatusCode == HttpStatusCode.OK)
                return true;

            return await RefreshToken();
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

            AuthenticationResponse authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await resp.Content.ReadAsStringAsync());
            Application.Current.Properties["accessToken"] = authenticationResponse.accessToken;
            Application.Current.Properties["refreshToken"] = authenticationResponse.refreshToken;
            await Application.Current.SavePropertiesAsync();
            return true;
        }

        public async Task<bool> RefreshToken()
        {
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("refresh_token", Application.Current.Properties["refreshToken"].ToString()));
            values.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y2l0aXplbjpzZWNyZXQ=");
            HttpResponseMessage resp = await _httpClient.PostAsync("https://oauth.citizen.navispeed.eu/oauth/token", new FormUrlEncodedContent(values));
            if (!resp.IsSuccessStatusCode)
                return false;

            AuthenticationResponse authenticationResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await resp.Content.ReadAsStringAsync());
            Application.Current.Properties["accessToken"] = authenticationResponse.accessToken;
            Application.Current.Properties["refreshToken"] = authenticationResponse.refreshToken;
            await Application.Current.SavePropertiesAsync();
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
            //Console.WriteLine("does refresh token work ? " + await RefreshToken());
           
           //req.Content = new StringContent("test");
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["accessToken"].ToString());
            HttpResponseMessage resp = await _httpClient.SendAsync(req);
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                if (await RefreshToken())
                    return ApiRequest(url, verb, JSONBody).Result;
           //TODO Check Result
            return await resp.Content.ReadAsStringAsync();
        }

        public async Task<string> UploadFile(string MIME, Stream fileStream)
        {
            try { 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Application.Current.Properties["accessToken"].ToString());
            HttpContent content = new StreamContent(fileStream);
            var response = await _httpClient.PostAsync("https://citizen.navispeed.eu/api/common/upload/file", content);
            return await response.Content.ReadAsStringAsync();
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}