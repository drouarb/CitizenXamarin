using System;
using citizen.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.Views;
using Plugin.LocalNotification;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace citizen
{
    public partial class App : Application
    {
        public static ApiService ApiService = new ApiService();
        public static NotificationService NotificationService = new NotificationService();

        public App()
        {
            InitializeComponent();
            
            MessagingCenter.Instance.Subscribe<LocalNotificationTappedEvent>(this,
                typeof(LocalNotificationTappedEvent).FullName, OnLocalNotificationTapped);

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
        
        private void OnLocalNotificationTapped(LocalNotificationTappedEvent e)
        {
            Console.WriteLine("Notification clicked");
        }
    }
}
