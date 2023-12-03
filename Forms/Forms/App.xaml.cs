using Xamarin.Forms;

namespace Forms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            CapCognition.Core.Shared.CapCognition.Initialize(new[]
            {
                //Add your licenses here
                "",
            });

            CapCognition.Core.Shared.CapCognition.EnableImageProcessingLogs = true;
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
