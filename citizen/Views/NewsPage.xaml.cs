using System;
using citizen.Models.Api;
using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {

        NewsDetailsViewModel newsDetailsViewModel;
        public NewsPage()
        {
            InitializeComponent();

            BindingContext = new NewsViewModel();
            newsDetailsViewModel = new NewsDetailsViewModel();
            newsDetailsViewModel.LoadNewsDetailsCommand.CanExecuteChanged += fetchNewsHandler;
            NewsListView.BeginRefresh();
        }

        void fetchNewsHandler(object sender, EventArgs a)
        {
            Console.WriteLine("nav to news");
            Navigation.PushAsync(new NewsDetailsView(newsDetailsViewModel));
        }

        void NewsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var news = e.SelectedItem as NewsItem;
            if (news == null)
                return;
            Console.WriteLine("fetching details news" + news.Uuid);
            newsDetailsViewModel.LoadNewsDetailsCommand.Execute(news.Uuid);
            Console.WriteLine("wat" + news.Uuid);
            NewsListView.SelectedItem = null;
        }
    }
}