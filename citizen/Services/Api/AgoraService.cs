using System;
using System.Collections.Generic;
using citizen.Models.Api;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace citizen.Services.Api
{
    public class AgoraService
    {
        private List<ThreadItem> threads;
        private List<PostItem> posts;
        private ThreadItem tr;

        public AgoraService()
        {
            threads = new List<ThreadItem>();
        }

        public AgoraService(ThreadItem thread)
        {
            tr = thread;
        }

        public async Task<String> CreateThreadAsync(String threadName)
        {
            string rawValue = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads?topic=" + threadName, HttpMethod.Post, null);
            Console.WriteLine("raw thread creqte:" + rawValue);
            return rawValue;
        }

        public async Task<ThreadItem> GetThreadAsync(String uuid)
        {
            string rawValue = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads/thread/" + uuid, HttpMethod.Get, null);
            Console.WriteLine("raw thread creqte:" + rawValue);
            return JsonConvert.DeserializeObject<ThreadItem>(rawValue);
        }

        public async Task<String> PostPostAsync(String post)
        {
            string rawValue = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads/posts?tid=" + tr.Uuid + "&author=user&message=" + post, HttpMethod.Post, null);
            return rawValue;
        }

        public async Task<IEnumerable<PostItem>> GetPostsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && posts.Count != 0)
                return posts;
            //TODO replace threads?pageNb=0&pageSize=100 by actual parameters
            string rawPosts = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads/thread/" + tr.Uuid + "/posts?pageNb=0&pageSize=100", HttpMethod.Get, null);
            Console.WriteLine("raw posts:" + rawPosts);
            posts = JsonConvert.DeserializeObject<List<PostItem>>(rawPosts);
            Console.WriteLine("post count" + posts.Count);
            return posts;
        }

        public async Task<IEnumerable<ThreadItem>> GetThreadsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && threads.Count != 0)
                return threads;

            //TODO replace threads?pageNb=0&pageSize=100 by actual parameters
            string rawThreads = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads?pageNb=0&pageSize=100", HttpMethod.Get, null);
            Console.WriteLine("raw threads:" + rawThreads);
            threads = JsonConvert.DeserializeObject<List<ThreadItem>>(rawThreads);
            Console.WriteLine("threads count" + threads.Count);
            threads.ForEach(thread =>
            {
                Console.WriteLine(thread.Topic);
                Console.WriteLine(thread.Created);
            });
            return threads;
        }
    }
}
