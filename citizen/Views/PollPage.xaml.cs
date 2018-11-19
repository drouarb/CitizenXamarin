using System;
using citizen.Models.Api;
using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollPage : ContentPage
    {
        public PollPage()
        {
            InitializeComponent();

            BindingContext = new PollViewModel();
            PollListView.BeginRefresh();
        }
        
        async void OnPollSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var poll = args.SelectedItem as PollItem;

            if (poll == null)
                return;

            Console.WriteLine("Navigate to poll " + poll.Uuid);

            await Navigation.PushAsync(new PollDetailPage(new PollDetailViewModel(poll)));
            
            PollListView.SelectedItem = null;
        }
    }
}
