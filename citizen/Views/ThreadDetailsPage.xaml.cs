using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.ViewModels;

namespace citizen.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ThreadDetailsPage : ContentPage
	{
        PostViewModel viewModel;

        public ThreadDetailsPage(PostViewModel viewModel)
        {
            Console.Write("load view model");
            InitializeComponent();
            Console.Write("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
            BindingContext = this.viewModel = viewModel;
        }
	}
}