using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class PollDetailsViewModel : BaseViewModel
    {
        public PollItem Poll { get; set; }
        public ObservableCollection<PollChoice> PollChoices { get; set; }
        public Command LoadChoicesCommand { get; set; }
        public Command<int> VoteCommand { get; set; }

        private PollDetailsService pollDetailsService;

        public PollDetailsViewModel(PollItem poll)
        {
            Poll = poll;
            Title = poll.Proposition;
            PollChoices = new ObservableCollection<PollChoice>();
            LoadChoicesCommand = new Command(async () => await ExecuteLoadChoicesCommand());
            VoteCommand = new Command<int>(async s => await ExecuteVote(s));
            pollDetailsService = new PollDetailsService(poll);
        }

        public async Task ExecuteVote(int selectedId)
        {
            IsBusy = true;
            PollChoice choice = PollChoices[selectedId];
            await pollDetailsService.Vote(choice);
            //VoteBusy = false;
            VoteCommand.ChangeCanExecute();
        }

        public async Task ExecuteLoadChoicesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                PollChoices.Clear();
                var items = await pollDetailsService.GetItemsAsync();
                foreach (var item in items)
                {
                    PollChoices.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION");
                Console.WriteLine(ex);
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            IsBusy = false;
        }
    }
}