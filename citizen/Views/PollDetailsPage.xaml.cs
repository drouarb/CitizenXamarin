using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using citizen.ViewModels;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollDetailsPage : ContentPage
    {
        PollDetailsViewModel viewModel;

        public PollDetailsPage(PollDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}