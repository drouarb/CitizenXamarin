using citizen.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumPage : ContentPage
    {
        public ForumPage()
        {
            InitializeComponent();
            BindingContext = new ForumTopicViewModel();

        }

        private void TopicListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}