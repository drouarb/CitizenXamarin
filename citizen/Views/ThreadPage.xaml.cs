using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.Models.Api;
using System;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThreadPage : ContentPage
    {
        ThreadViewModel viewModel;

        public ThreadPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ThreadViewModel();
            ThreadListView.BeginRefresh();
        }

        async void OnThreadSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var thread = args.SelectedItem as ThreadItem;

            if (thread == null)
                return;

            try {
            var postview = new PostViewModel(thread);
            var threadetails = new ThreadDetailsPage(postview);
            await Navigation.PushAsync(new ThreadDetailsPage(new PostViewModel(thread)));

            ThreadListView.SelectedItem = null;
            } catch (Exception e) {
                Console.WriteLine("exception exception" + e.Message); 
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Threads.Count == 0)
                viewModel.LoadThreadCommand.Execute(null);
        }
    }
}