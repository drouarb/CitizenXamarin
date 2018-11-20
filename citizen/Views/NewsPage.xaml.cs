using citizen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void NewsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}