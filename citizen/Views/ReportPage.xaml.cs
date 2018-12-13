using citizen.Models.Api;
using citizen.Services;
using citizen.Services.Api;
using citizen.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        ReportService ReportService = new ReportService();
        ReportViewModel ReportViewModel = new ReportViewModel();
        private bool imageLoaded = false;

        public ReportPage()
        {
            ReportViewModel.sendReportCommand.CanExecuteChanged += ReportSentHandler;
            ReportViewModel.SubmitIndicator = SubmitIndicator;
            ReportViewModel.SubmitButton = SubmitButton;
            ReportViewModel.TakePhotoButton = TakePhotoButton;
            ReportViewModel.PickPhotoButton = PickPhotoButton;
            ReportViewModel.title = Title;
            ReportViewModel.Description = Description;
            ReportViewModel.image = image;
            ReportViewModel.ImageRow = ImageRow;
            InitializeComponent();
        }

        public void TakePhotoHandler(object sender, EventArgs e)
        {
            ReportViewModel.takePhotoCommand.Execute(null);
        }

        public void PickPhotoHandler(object sender, EventArgs e)
        {
            ReportViewModel.pickPhotoCommand.Execute(null);
        }

        public void SendReportHandler(object sender, EventArgs e)
        {
            ReportViewModel.sendCommand.Execute(null);
        }

        public void ReportSentHandler(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                SubmitButton.Text = "Envoyer le Signalement";
                SubmitButton.IsEnabled = true;
                SubmitIndicator.IsRunning = false;
                SubmitIndicator.IsVisible = false;
                Description.IsEnabled = true;
                Description.Text = "";
                Title.IsEnabled = true;
                Title.Text = "";
                TakePhotoButton.IsEnabled = true;
                PickPhotoButton.IsEnabled = true;
                ReportViewModel.file = null;
                image.Source = null;
                imageLoaded = false;
                ImageRow.Height = 0;

                DisplayAlert("Signalement envoyé", "Merci pour votre signalement", "OK");
            });
        }

        public void KeyboardChangeHandler(bool isShown)
        {
            if (isShown)
            {
                ImageRow.Height = 0;
                SubmitGrid.IsVisible = false;
            }
            else
            {
                ImageRow.Height = imageLoaded ? GridLength.Star : 0;
                SubmitGrid.IsVisible = true;
            }

            ForceLayout();
        }
    }
}