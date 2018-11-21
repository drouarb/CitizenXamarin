using System;
using citizen.ViewModels;
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
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameEntry.Text) || string.IsNullOrEmpty(PasswordEntry.Text))
            {
                MessageLabel.Text = "Empty username/password";
                MessageLabel.Opacity = 100;
                return;
            }
            
            LoginButton.Text = "";
            MessageLabel.Opacity = 0;
            LoginButton.IsEnabled = false;
            _loginViewModel.IsBusy = true;

            if (await App.ApiService.Authenticate(UsernameEntry.Text, PasswordEntry.Text))
            {
                App.Current.MainPage = new MainPage();
            }
            else
            {
                MessageLabel.Text = "Invalid username/password";
                MessageLabel.Opacity = 100;
            
                LoginButton.IsEnabled = true;
                _loginViewModel.IsBusy = false;
                LoginButton.Text = "Login";
            }
        }
    }
}
