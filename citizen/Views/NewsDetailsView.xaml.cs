using citizen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
			htmlSource.Html = "<html>" +
			                  "		<head>" +
			                  "			<link href='https://v1.grommet.io/assets/css/index-vanilla.css' rel='stylesheet' type='text/css'>" +
			                  "			<link href='http://192.168.1.30/style.css' rel='stylesheet' type='text/css'>" +
			                  "		</head>" +
			                  "		<body>" +
			                  "		<div style='width:90%;margin: 0 auto;' >" +
			                  "			<h1 class='grommetux-heading grommetux-heading--align-center'>" + viewModel.news.title + "</h2>" +
			                  "			<h2 class='grommetux-heading grommetux-heading--align-center'>" + viewModel.news.subtitle + "</h3>" +
			                  Regex.Replace(viewModel.news.content, @"\<img ", "<img style='width:110%;margin-left:-5%;' ") +
			                  "		</div>" +
			                  "		</body>" +
			                  "</html>";
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