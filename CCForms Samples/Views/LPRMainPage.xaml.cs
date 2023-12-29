using CapCognition.Core.Shared.Common;
using CapCognition.LicensePlateDetection.Shared;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCForms_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LPRMainPage : ContentPage
    {
        public LPRMainPage()
        {
            InitializeComponent();
            BindingContext = this;
            Title = "License Plate Recognition";

            _licensePlateOptions = new LicensePlateDetectionRecognitionOption()
            {
                DisplayLicensePlateAsText = true,
                UseHighResolutionImageForRecognition = false,
                UseCroppedImageForRecognition = true,
            };
            RecognitionView.AddOption(_licensePlateOptions);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_licensePlateOptions != null)
            {
                _licensePlateOptions.PreparationProgressEvt += OnLicensePlatePreparationProgress;
                await _licensePlateOptions.CreateAndPrepareModelAsync();
                _licensePlateOptions.PreparationProgressEvt -= OnLicensePlatePreparationProgress;
            }

            await RecognitionView.RequestAllPermissionAsync();
            await RecognitionView.OpenCameraAsync();
            RecognitionView.StartIntervalRecognition(20);
            RecognitionView.RecognitionResultEvt += OnRecognition;
        }

        private void OnLicensePlatePreparationProgress(long noOfReadBytes, int fileIdx, int totalNoOfFiles)
        {
            Console.WriteLine($"License plate model {fileIdx + 1} of {totalNoOfFiles} loading. {noOfReadBytes} bytes read");
        }

        private void OnRecognition(RecognitionResult obj)
        {
            if (obj == null)
            {
                return;
            }

            var licensePlates = obj.Results;
            if (licensePlates != null)
            {
                //Cast to LicensePlateDetectionResult
                foreach (var licensePlate in licensePlates)
                {
                    if (licensePlate is RecognitionProcessorLicensePlateDetectionResult lprResult)
                    {
                        Console.WriteLine($"Plate number: {lprResult.PlateNumber}");
                        Console.WriteLine($"Country: {lprResult.PlateCountryCode}");
                        Console.WriteLine($"Vehicle type: {lprResult.Type}");
                    }
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            RecognitionView.CloseCamera();
            RecognitionView.StopIntervalRecognition();
        }

        private LicensePlateDetectionRecognitionOption _licensePlateOptions;
    }
}