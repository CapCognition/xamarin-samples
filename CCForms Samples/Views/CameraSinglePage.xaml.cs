using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCForms_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraSinglePage : ContentPage
    {
        public CameraSinglePage()
        {
            InitializeComponent();
            BindingContext = this;

            Title = "Camera Single";
            CaptureView.PropertyChanged += CaptureViewOnPropertyChanged;
        }

        protected override async void OnAppearing()
        {
            await CaptureView.OpenCameraAsync();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            CaptureView.CloseCamera();
            base.OnDisappearing();
        }

        public int FlashOpacity => CaptureView.TorchIsOn ? 100 : 50;

        //--------------------------------------------------------------------------
        private ImageSource _capturedImageSource;

        public ImageSource CapturedImageSource
        {
            get => _capturedImageSource;
            set
            {
                if (_capturedImageSource == value)
                {
                    return;
                }

                _capturedImageSource = value;
                OnPropertyChanged();
            }
        }

        private void OnTakePhotoClicked(object sender, EventArgs e)
        {
            Task.Factory.StartNew(async () =>
            {
                var skBitmap = await CaptureView.CaptureSingleImageAsync();
                var imgSource = (SKBitmapImageSource)skBitmap;

                Device.BeginInvokeOnMainThread(() =>
                {
                    CapturedImageSource = imgSource;
                });
            });
        }

        private void CaptureViewOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CaptureView.TorchIsOn))
            {
                OnPropertyChanged(nameof(FlashOpacity));
            }
        }

        private async void CaptureButton_OnPressed(object sender, EventArgs e)
        {
            await CaptureButton.ScaleTo(0.9, 50);
        }

        private void CaptureButton_OnReleased(object sender, EventArgs e)
        {
            CaptureButton.ScaleTo(1, 50);
        }

        private void OnToggleTorch(object sender, EventArgs e)
        {
            CaptureView.SetTorch(!CaptureView.TorchIsOn);
        }

        private async void OnChangeCamera(object sender, EventArgs e)
        {
            //TODO this is not working, will be updated in next nuget release

            CaptureView.CloseCamera();
            CaptureView.UseFrontCamera = !CaptureView.UseFrontCamera;

            await CaptureView.OpenCameraAsync();
        }
    }
}