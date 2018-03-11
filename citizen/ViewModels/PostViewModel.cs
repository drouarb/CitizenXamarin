using citizen.Models.Api;
using citizen.Services.Api;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class PostViewModel : BaseViewModel
    {
        public ThreadItem Thread { get; set; }
        public ObservableCollection<PostItem> Posts { get; set; }
        private AgoraService AgoraService;
        public Command LoadPostCommand { get; set; }
        public Command SendPostCommand { get; set; }

        public PostViewModel(ThreadItem thread)
        {
            Console.Write("postviewmodel load\n");
            Thread = thread;
            AgoraService = new AgoraService(thread);
            Title = thread.Topic;
            Posts = new ObservableCollection<PostItem>();
            LoadPostCommand = new Command(async () => await ExecuteLoadPostCommand());
            SendPostCommand = new Command<String>(async (UserPost) => await ExecuteSubmitPostAsync(UserPost));
        }

        internal async Task ExecuteSubmitPostAsync(string UserPost)
        {
            await AgoraService.PostPostAsync(UserPost);
            await ExecuteLoadPostCommand();
        }

        async Task ExecuteLoadPostCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                Posts.Clear();
                var items = await AgoraService.GetPostsAsync(true);
                foreach (var item in items)
                {
                    item.Author = await AgoraService.GetUserNameById(item.Author);
                    Posts.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
