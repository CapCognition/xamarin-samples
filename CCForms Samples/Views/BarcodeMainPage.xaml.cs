using System.Collections.Generic;
using CapCognition.BarcodeScanning.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = System.Drawing.Color;

namespace CCForms_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeMainPage : ContentPage
    {
        public BarcodeMainPage()
        {
            InitializeComponent();
            BindingContext = this;
            Title = "Barcode";

            RecognitionView.AddOption(new BarcodeRecognitionOption
            {
                BinarizerToUse = BarcodeRecognitionOption.BinarizerType.HistogrammBinarizer,
                BarcodeFormatsToRecognize = new List<BarcodeRecognitionOption.BarcodeFormat>()
                {
                    BarcodeRecognitionOption.BarcodeFormat.QRCode
                },
                DisplayBarcodeText = false,
                EnableMultiCodeReader = true,
                SurroundingRectColor = Color.Red,
                SurroundingRectStrokeWidth = 3,
                EnableBarcodeOverlays = true,
                UseFastRecognition = false
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await RecognitionView.RequestAllPermissionAsync();
            await RecognitionView.OpenCameraAsync();
            RecognitionView.StartIntervalRecognition(20);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            RecognitionView.CloseCamera();
            RecognitionView.StopIntervalRecognition();
        }
    }
}