using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.Models.Api;
using System;
using Acr.UserDialogs;

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
            viewModel.CreateThreadCommand.CanExecuteChanged += FetchThreadHandler;

            create.Clicked += async (sender, args) =>
            {
                PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                {
                    InputType = InputType.Name,
                    OkText = "Créer",
                    Title = "Créer un nouveau sujet"
                });
                if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                {
                    viewModel.CreateThreadCommand.Execute(pResult.Text);
                }
            };
        }

        void FetchThreadHandler(object sender, EventArgs e)
        {
            Console.WriteLine("getlastthread");
            var thread = viewModel.getLastThread();
            Console.WriteLine("postviewmodel" + thread);
            var postview = new PostViewModel(thread);
            Console.WriteLine("ThreadDetailsPage");
            Navigation.PushAsync(new ThreadDetailsPage(new PostViewModel(thread)));
        }

        async void OnThreadSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var thread = args.SelectedItem as ThreadItem;

            if (thread == null)
                return;

            try {
            var postview = new PostViewModel(thread);
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