using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCForms_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureDetailPage : ContentPage
    {
        public PictureDetailPage(ImageSource imageSource)
        {
            CapturedImage = imageSource;
            InitializeComponent();
            BindingContext = this;

            Title = "Picture Details";
        }

        //--------------------------------------------------------------------------
        private ImageSource _capturedImage;

        public ImageSource CapturedImage
        {
            get => _capturedImage;
            set
            {
                if (_capturedImage == value)
                {
                    return;
                }

                _capturedImage = value;
                OnPropertyChanged();
            }
        }
    }
}