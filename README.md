# xamarin-samples

<img src="/assets/main.png" alt="Mainscreen" height="600"/>

Shows the usage:
- How to capture a single images in RAW format
- How to capture a sequence of images in RAW format
- How to recognize Barcodes and QR-Codes
- How to recognize license plates

Please note that the library must download the model when the license plate recognition is started for the first time before recognition begins.
The loading progress of this model could be displayed in the corresponding event handler OnLicensePlatePreparationProgress(). However, this has been omitted in this example.
The library also offers the option of loading the model in advance or in the background so that there is no delay when the recognition process is used.
