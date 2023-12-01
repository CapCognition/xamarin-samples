using Xamarin.Forms;

namespace Forms.Views
{
    public partial class BarcodeMainPage : ContentPage
    {
        public BarcodeMainPage()
        {
            InitializeComponent();
            BindingContext = this;
            BackgroundColor = Color.FromHex("#2c2c2c");
            Title = "Barcode options";
        }
    }
}