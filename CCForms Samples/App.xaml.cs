using CCForms_Samples.Views;
using Xamarin.Forms;

namespace CCForms_Samples
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());

            CapCognition.Core.Shared.CapCognition.Initialize(new[]
            {
                //Add your licenses here
                "",
            });

            //Enable logs
            //CapCognition.Core.Shared.CapCognition.EnableImageProcessingLogs = true;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
