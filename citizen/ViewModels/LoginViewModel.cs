using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace citizen.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public String Error = "";
        public bool Authenticated = false;
        public Command<Tuple<string, string>> AuthenticateCommand { get; set; }
        public Command CheckIsAuthenticatedCommand { get; set; }
        public LoginViewModel()
        {
            AuthenticateCommand = new Command<Tuple<string, string>>(async creds => await ExecuteAuthentication(creds));
            CheckIsAuthenticatedCommand = new Command(async () => await ExecuteCheckIsAuthenticated());
        }

        private async Task ExecuteCheckIsAuthenticated()
        {
            if (IsBusy)
                return;
            
            IsBusy = true;
            Authenticated = await App.ApiService.IsAuthenticated();
            IsBusy = false;
            CheckIsAuthenticatedCommand.ChangeCanExecute();
        }

        private async Task ExecuteAuthentication(Tuple<string, string> creds)
        {
            if (IsBusy)
                return;
            
            IsBusy = true;
            Authenticated = await App.ApiService.Authenticate(creds.Item1, creds.Item2);
            Error = "Identifiants invalides";
            IsBusy = false;
            AuthenticateCommand.ChangeCanExecute();
        }
    }
}