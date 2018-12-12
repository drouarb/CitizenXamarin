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
        MediaFile file;
        private bool imageLoaded = false;
        ReportService ReportService = new ReportService();
        ReportViewModel ReportViewModel = new ReportViewModel();
        ReportContentItem content = new ReportContentItem();
        public ReportPage()
        {
            InitializeComponent();
            ReportViewModel.sendReportCommand.CanExecuteChanged += ReportSentHandler;
        }

        public async void TakePhotoHandler(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            imageLoaded = true;
            ImageRow.Height = GridLength.Star;
        }

        public async void PickPhotoHandler(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });


            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            imageLoaded = true;
            ImageRow.Height = GridLength.Star;
        }

        public async void SendReportHandler(object sender, EventArgs e)
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (hasPermission != PermissionStatus.Granted)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            content.title = Title.Text;
            content.description = Description.Text;
            content.img_uuid = null;
            content.lat = 0;
            content.lon = 0;

            if (String.IsNullOrEmpty(Title.Text))
            {
                DisplayAlert("Erreur", "Veuillez spécifier un titre", "OK");
                return;
            }

            if (String.IsNullOrEmpty(Description.Text))
            {
                DisplayAlert("Erreur", "Veuillez spécifier une description", "OK");
                return;
            }

            if (file == null)
            {
                DisplayAlert("Erreur", "Veuillez sélectionner une photo", "OK");
                return;
            }

            SubmitButton.Text = "";
            SubmitButton.IsEnabled = false;
            SubmitIndicator.IsVisible = true;
            SubmitIndicator.IsRunning = true;
            Description.IsEnabled = false;
            Title.IsEnabled = false;
            TakePhotoButton.IsEnabled = false;
            PickPhotoButton.IsEnabled = false;

            await Task.Factory.StartNew(async () =>
            {
                content.img_uuid = await App.ApiService.UploadFile("image/jpeg", file.GetStream());
                Console.WriteLine("Reports inc +++ " + content.img_uuid);
                Console.WriteLine("geoloc available ?" + CrossGeolocator.Current.IsGeolocationAvailable);
                if (CrossGeolocator.Current.IsGeolocationAvailable)
                {
                    Console.WriteLine(" GPS yesss");
                    Position pos = await CrossGeolocator.Current.GetPositionAsync();
                    content.lat = pos.Latitude;
                    content.lon = pos.Longitude;
                }
                else
                {
                    Console.WriteLine("GPS unavailable");
                }

                Console.WriteLine("geoloc; " + content.lat + content.lon);
                ReportViewModel.sendReportCommand.Execute(content);
            });
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
                file = null;
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