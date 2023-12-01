using Xamarin.Forms;

namespace Forms.Views
{
    public partial class LPRMainPage : ContentPage
    {
        public LPRMainPage()
        {
            InitializeComponent();
            BindingContext = this;
            BackgroundColor = Color.FromHex("#2c2c2c");
            Title = "LPR options";
        }
    }
}