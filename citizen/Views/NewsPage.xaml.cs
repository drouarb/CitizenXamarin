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
        public NewsPage()
        {
            InitializeComponent();

            BindingContext = new NewsViewModel();
            NewsListView.BeginRefresh();
        }

        async void NewsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var news = e.SelectedItem as NewsItem;

            if (news == null)
                return;

            Console.WriteLine("Navigate to news " + news.Uuid);

            await Navigation.PushAsync(new NewsDetailsView(new NewsDetailsViewModel(news)));

            NewsListView.SelectedItem = null;
        }
    }
}