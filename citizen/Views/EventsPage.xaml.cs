using System;
using citizen.Models.Api;
using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        EventViewModel viewModel;

		public EventsPage()
		{
            InitializeComponent();
            BindingContext = viewModel = new EventViewModel();
            EventListView.BeginRefresh();
        }

        void FetchEventDetailHandler(object sender, EventArgs e)
        {
            //TODO details 
        }
        
        async void OnEventSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null)
                return;
            var _event = args.SelectedItem as EventsItem;
            await Navigation.PushAsync(new EventDetailPage(_event));
            EventListView.SelectedItem = null;
        }
    }
}