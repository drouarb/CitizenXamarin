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

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Factory.StartNew(async () => App.NotificationService.FetchNotifications());
                return true;
            });
        }
    }
}