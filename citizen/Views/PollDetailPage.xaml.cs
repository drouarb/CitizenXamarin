using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using citizen.ViewModels;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollDetailPage : ContentPage
    {
        PollDetailViewModel viewModel;

        public PollDetailPage(PollDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}