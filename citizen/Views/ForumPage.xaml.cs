using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumPage : ContentPage
    {
        ForumTopicViewModel viewModel;

        public ForumPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ForumTopicViewModel();

        }

        private void TopicListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Threads.Count == 0)
                viewModel.LoadTopicCommand.Execute(null);
        }
    }
}