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

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        async void OnEventSelected(object sender, SelectedItemChangedEventArgs args)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            //TODO details 
        }
    }
}