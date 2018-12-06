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
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        MediaFile file;
        ReportService ReportService = new ReportService();
        ReportViewModel ReportViewModel = new ReportViewModel();

        public ReportPage()
        {
            InitializeComponent();
            CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            takePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
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

                DisplayAlert("File Location", file.Path, "OK");

                image.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            };

            send.Clicked += async (sender, args) =>
            {
                ReportContentItem content = new ReportContentItem();
                content.title = Title.Text;
                content.description = Description.Text;
                content.img_uuid = null;
                content.lat = 0;
                content.lon = 0;
                if (file != null) {
                    await Task.Factory.StartNew(async () => { 
                    content.img_uuid = await App.ApiService.UploadFile("image/jpeg", file.GetStream());
                    Console.WriteLine("Reports inc +++ " + content.img_uuid);
                        Console.WriteLine("geoloc available ?" + CrossGeolocator.Current.IsGeolocationAvailable);
                    if (CrossGeolocator.Current.IsGeolocationAvailable)
                    {
                            DisplayAlert("No GPS", ":( no gps.", "OK");
                            Position pos =  await CrossGeolocator.Current.GetPositionAsync();
                        content.lat = pos.Latitude;
                        content.lon = pos.Longitude;
                    }
                     else   { DisplayAlert("GPS available", ":( yessss.", "OK"); }
                    Console.WriteLine("geoloc; " + content.lat + content.lon);
                    ReportViewModel.sendReportCommand.Execute(content);
                    });
                }

            };

            pickPhoto.Clicked += async (sender, args) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
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
            };

            /*           takeVideo.Clicked += async (sender, args) =>
                       {
                           if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
                           {
                               DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                               return;
                           }

                           var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
                           {
                               Name = "video.mp4",
                               Directory = "DefaultVideos",
                           });

                           if (file == null)
                               return;

                           DisplayAlert("Video Recorded", "Location: " + file.Path, "OK");

                           file.Dispose();
                       };

                       pickVideo.Clicked += async (sender, args) =>
                       {
                           if (!CrossMedia.Current.IsPickVideoSupported)
                           {
                               DisplayAlert("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                               return;
                           }
                           var file = await CrossMedia.Current.PickVideoAsync();

                           if (file == null)
                               return;

                           DisplayAlert("Video Selected", "Location: " + file.Path, "OK");
                           file.Dispose();
                       };*/
        }

        public ReportPage(Button takePhoto, Button pickPhoto, Button takeVideo, Button pickVideo, Image image)
        {
            this.takePhoto = takePhoto;
            this.pickPhoto = pickPhoto;
            /*this.takeVideo = takeVideo;
            this.pickVideo = pickVideo;*/
            this.image = image;
        }
    }
}
