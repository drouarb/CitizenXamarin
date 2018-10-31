using System;
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
        }
        
        async void OnPollSelected(object sender, SelectedItemChangedEventArgs args)
        {
        }
    }
}
