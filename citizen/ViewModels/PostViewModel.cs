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

        public PostViewModel(ThreadItem thread)
        {
            Console.Write("postviewmodel load\n");
            Thread = thread;
            AgoraService = new AgoraService(thread);
            Console.Write("agora\n");
            Title = thread.Topic;
            Console.Write("yepyepyepyep\n");
            Posts = new ObservableCollection<PostItem>();
            LoadPostCommand = new Command(async () => await ExecuteLoadPostCommand());
        }

        internal async Task ExecuteSubmitPostAsync(string UserPost)
        {
            await AgoraService.PostPostAsync(UserPost);
        }

        async Task ExecuteLoadPostCommand()
        {
            Console.Write("execute\n");
            if (IsBusy)
                return;

            IsBusy = true;
            Console.Write("try\n");
            try
            {
                Posts.Clear();
                var items = await AgoraService.GetPostsAsync(true);
                Console.Write("lets do dis\n");
                foreach (var item in items)
                {
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
            Console.Write("we did it\n");
        }
    }
}
