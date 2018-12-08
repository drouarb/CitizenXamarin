using System;
using citizen.Services;
using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.Views;
using Plugin.LocalNotification;
using Xamarin.Forms.Internals;

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
                typeof(LocalNotificationTappedEvent).FullName, NotificationRouter);
            
            MainPage = new LoginPage();
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

        private void NotificationRouter(LocalNotificationTappedEvent e)
        {
            var route = e.Data.Split('/');
            switch (route[1])
            {
                case "consultation":
                    try
                    {
                        Console.WriteLine("Notification Poll " + route[2]);
                        ((MainPage) MainPage).CurrentPage.Navigation.PushAsync(new PollDetailsPage(route[2]));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    return;
            }
        }
    }
}