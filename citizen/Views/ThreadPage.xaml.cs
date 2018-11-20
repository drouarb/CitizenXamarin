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

            Console.WriteLine("Navigate to thread " + thread.Uuid);

            await Navigation.PushAsync(new ThreadDetailsPage(new ThreadDetailsViewModel(thread)));

            ThreadListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Threads.Count == 0)
                viewModel.LoadTopicCommand.Execute(null);
        }
    }
}