using System;
using citizen.ViewModels;
using Plugin.LocalNotification;
using Xamarin.Forms;

namespace citizen.Views
{
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _loginViewModel;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = _loginViewModel = new LoginViewModel();
            _loginViewModel.AuthenticateCommand.CanExecuteChanged += AuthenticationCallbackHandler;
            _loginViewModel.CheckIsAuthenticatedCommand.CanExecuteChanged += AuthenticationCallbackHandler;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoginButton.Text = "";
            LoginButton.IsEnabled = false;
            _loginViewModel.CheckIsAuthenticatedCommand.Execute(null);
        }

        void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                MessageLabel.Text = "Veuillez renseigner tout les champs";
                MessageLabel.Opacity = 100;
                return;
            }
            
            LoginButton.Text = "";
            MessageLabel.Opacity = 0;
            LoginButton.IsEnabled = false;
            _loginViewModel.AuthenticateCommand.Execute(new Tuple<string, string>(UsernameEntry.Text, PasswordEntry.Text));
        }

        protected void AuthenticationCallbackHandler(object sender, EventArgs e)
        {
            if (_loginViewModel.Authenticated)
            {
                App.Current.MainPage = new MainPage();
                return;
            }
            
            LoginButton.IsEnabled = true;
            LoginButton.Text = "Login";

            if (!String.IsNullOrEmpty(_loginViewModel.Error))
            {
                MessageLabel.Opacity = 100;
                MessageLabel.Text = _loginViewModel.Error;
            }
        }
    }
}
