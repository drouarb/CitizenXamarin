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

        public AgoraService()
        {
            threads = new List<ThreadItem>();
        }

        public AgoraService(ThreadItem thread)
        {

        }

        public async Task<IEnumerable<PostItem>> GetThreadDetailsAsync(bool forceRefresh = false)
        {
            Console.WriteLine("get threads called bb");
            //TODO replace threads?pageNb=0&pageSize=100 by actual parameters
            string rawPosts = await App.ApiService.ApiRequest("https://citizen.navispeed.eu/api/threads?pageNb=0&pageSize=100", HttpMethod.Get, null);
            Console.WriteLine("raw posts:" + rawPosts);
            posts = JsonConvert.DeserializeObject<List<PostItem>>(rawPosts);
            Console.WriteLine("threads count" + threads.Count);
            return posts;
        }

        public async Task<IEnumerable<ThreadItem>> GetThreadsAsync(bool forceRefresh = false)
        {
            if (forceRefresh == false && threads.Count != 0)
                return await Task.FromResult(threads);

            Console.WriteLine("get threads called bb");
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
            return await Task.FromResult(threads);
        }
    }
}
