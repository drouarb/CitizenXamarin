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
			InitializeComponent ();

            BindingContext = this.viewModel = viewModel;
		}
	}
}