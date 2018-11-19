using citizen.Models.Api;
using citizen.Services.Api;

namespace citizen.ViewModels
{
    public class PollDetailsViewModel : BaseViewModel
    {
        public PollItem Poll { get; set; }
        private PollDetailsService pollDetailsService;
        
        public PollDetailsViewModel(PollItem poll)
        {
            Poll = poll;
            this.pollDetailsService = new PollDetailsService(poll);
            Title = poll.Proposition;
        }
    }
}