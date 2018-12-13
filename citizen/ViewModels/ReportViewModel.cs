using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using citizen.Services;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using System;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using citizen.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Flex.Controls;

namespace citizen.ViewModels
{
    class ReportViewModel: BaseViewModel
    {
        public Command sendReportCommand { get; set; }
        public Command takePhotoCommand { get; set; }
        public Command pickPhotoCommand { get; set; }
        public Command sendCommand { get; set; }
        ReportContentItem content = new ReportContentItem();

        public ActivityIndicator SubmitIndicator;
        public FlexButton SubmitButton;
        public FlexButton TakePhotoButton;
        public FlexButton PickPhotoButton;
        public Entry title;
        public Editor Description;
        public Image image;
        public RowDefinition ImageRow;
        public ReportService ReportService = new ReportService();
        public ReportPage page;
        public MediaFile file;

        private bool imageLoaded = false;




        public ReportViewModel()
        {
            IsBusy = false;
            Title = "Signalement";
            sendReportCommand = new Command<ReportContentItem>(async (content) => await ExecuteSendReportCommand(content));
            takePhotoCommand = new Command(async () => await ExecuteTakePhotoCommand());
            pickPhotoCommand = new Command(async () => await ExecutePickPhotoCommand());
            sendCommand = new Command(async () => await ExecuteSendCommand());
        }

        async Task ExecuteTakePhotoCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await page.DisplayAlert("No Camera", ":( No camera available.", "OK");
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

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            imageLoaded = true;
            ImageRow.Height = GridLength.Star;
        }

        async Task ExecutePickPhotoCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await page.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
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

        async Task ExecuteSendCommand()
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (hasPermission != PermissionStatus.Granted)
            {
                await page.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            content.title = title.Text;
            content.description = Description.Text;
            content.img_uuid = null;
            content.lat = 0;
            content.lon = 0;

            if (String.IsNullOrEmpty(title.Text))
            {
                page.DisplayAlert("Erreur", "Veuillez spécifier un titre", "OK");
                return;
            }

            if (String.IsNullOrEmpty(Description.Text))
            {
                page.DisplayAlert("Erreur", "Veuillez spécifier une description", "OK");
                return;
            }

            if (file == null)
            {
                page.DisplayAlert("Erreur", "Veuillez sélectionner une photo", "OK");
                return;
            }

            SubmitButton.Text = "";
            SubmitButton.IsEnabled = false;
            SubmitIndicator.IsVisible = true;
            SubmitIndicator.IsRunning = true;
            Description.IsEnabled = false;
            title.IsEnabled = false;
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
                sendReportCommand.Execute(content);
            });
        }

        async Task ExecuteSendReportCommand(ReportContentItem content)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await ReportService.ReportPostAsync(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
                sendReportCommand.ChangeCanExecute();
            }

        }
    }
}

