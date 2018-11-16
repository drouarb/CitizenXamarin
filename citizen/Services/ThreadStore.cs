using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using citizen.Models;

namespace citizen.Services
{
    public class ThreadStore : IDataStore<ThreadItem>
    {
        List<ThreadItem> threads;

        public ThreadStore()
        {
            threads = new List<ThreadItem>();
            threads = App.ApiService.GetThreads().Result;
        }

        public Task<bool> AddItemAsync(ThreadItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ThreadItem> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ThreadItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.ApiService.GetThreads();
        }

        public Task<bool> UpdateItemAsync(ThreadItem item)
        {
            throw new NotImplementedException();
        }
    }
}
