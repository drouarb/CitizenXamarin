using System;
using citizen.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace citizen
{
    public partial class App : Application
    {
        public static ApiService ApiService = new ApiService();

        public App()
        {
            InitializeComponent();

            if (!ApiService.IsAuthenticated())
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
