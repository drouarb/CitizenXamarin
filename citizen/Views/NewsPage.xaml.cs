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
            NewsListView.BeginRefresh();
        }

        void NewsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var news = e.SelectedItem as NewsItem;
            if (news == null)
                return;
            Navigation.PushAsync(new NewsDetailsView(new NewsDetailsViewModel(news.Uuid, news.title)));
            NewsListView.SelectedItem = null;
        }
    }
}