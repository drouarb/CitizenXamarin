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
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            UserPost.AutoSize = EditorAutoSizeOption.TextChanges;
            PostListView.BeginRefresh();
        }

        private void HandleSubmit(object sender, EventArgs e)
        {
            if (!viewModel.IsBusy) {
                viewModel.SendPostCommand.Execute(UserPost.Text);
                UserPost.Text = "";
            }
        }

        private void HandleItemSelected(object sender, EventArgs e)
        {
            PostListView.SelectedItem = null;
        }
    }
}