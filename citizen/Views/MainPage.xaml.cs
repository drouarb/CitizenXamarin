using System;
using System.Threading.Tasks;
using citizen.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Task.Factory.StartNew(async () => await App.NotificationService.FetchNotifications());
            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
                Console.WriteLine("Notifs Fetch");
                Task.Factory.StartNew(async () => await App.NotificationService.FetchNotifications());
                return true;
            });
        }
    }
}