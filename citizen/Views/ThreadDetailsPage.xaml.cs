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
        ThreadDetailsViewModel viewModel;

        public ThreadDetailsPage(ThreadDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
	}
}