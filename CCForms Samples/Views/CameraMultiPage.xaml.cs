using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = Xamarin.Forms.Image;

namespace CCForms_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraMultiPage : ContentPage
    {
        public CameraMultiPage()
        {
            InitializeComponent();
            BindingContext = this;

            Title = "Interval";
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

        public string IntervalButtonText => CaptureView.CapturePending ? "Stop Interval" : "Start Interval";

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

        private void OnStartIntervalClicked(object sender, EventArgs e)
        {
            if (!CaptureView.CapturePending)
            {
                CaptureView.ImageCapturedEvt += OnIntervalImageCaptured;
                CaptureView.StartIntervalCapturing(500);
            }
            else
            {
                CaptureView.StopCapturing();
                CaptureView.ImageCapturedEvt -= OnIntervalImageCaptured;
            }
        }

        private void OnIntervalImageCaptured(SKBitmap bmp)
        {
            Task.Factory.StartNew(async () =>
            {
                var imgSource = (SKBitmapImageSource)bmp;

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

            if (e.PropertyName == nameof(CaptureView.CapturePending))
            {
                OnPropertyChanged(nameof(IntervalButtonText));
            }
        }

        private async void CaptureButton_OnPressed(object sender, EventArgs e)
        {
            await IntervalButton.ScaleTo(0.9, 50);
        }

        private void CaptureButton_OnReleased(object sender, EventArgs e)
        {
            IntervalButton.ScaleTo(1, 50);
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

        private void OnPreviewImageTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PictureDetailPage(CapturedImageSource));
        }
    }
}