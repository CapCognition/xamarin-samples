using Xamarin.Forms;

namespace Forms.Views
{
    public partial class CameraMainPage : ContentPage
    {
        public CameraMainPage()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#2c2c2c");
            BindingContext = this;
            Title = "Camera options";
        }
    }
}