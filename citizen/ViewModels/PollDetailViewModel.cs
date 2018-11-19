using citizen.Models.Api;

namespace citizen.ViewModels
{
    public class PollDetailViewModel : BaseViewModel
    {
        private PollItem poll;
        
        public PollDetailViewModel(PollItem poll)
        {
            this.poll = poll;
            Title = poll.Proposition;
        }
    }
}