using citizen.Models.Api;
using citizen.Services.Api;

namespace citizen.ViewModels
{
    public class ThreadDetailsViewModel : BaseViewModel
    {
        public ThreadItem Thread { get; set; }
        private AgoraService AgoraService;

        public ThreadDetailsViewModel(ThreadItem thread)
        {
            Thread = thread;
            this.AgoraService = new AgoraService(thread);
            Title = thread.Topic;
        }
    }
}
