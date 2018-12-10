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
	public partial class NewsDetailsView : ContentPage
	{
        NewsDetailsViewModel viewModel;

		public NewsDetailsView (NewsDetailsViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            this.viewModel.LoadNewsDetailsCommand.CanExecuteChanged += HandleNewsLoaded;
		}

		public void HandleNewsLoaded(object sender, EventArgs e)
		{
			var browser = new WebView();
			var htmlSource = new HtmlWebViewSource();
			htmlSource.Html = "<h2>" + viewModel.news.title + "</h2>" + "<h3>" + viewModel.news.subtitle + "</h3>" + viewModel.news.content;
			browser.Source = htmlSource;
			Content = browser;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            viewModel.LoadNewsDetailsCommand.Execute(null);
        }
    }
}