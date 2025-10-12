using BloodPressureApp.Views;

namespace BloodPressureApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new AppShell());

            // Temporary to check login page
            return new Window(new Login());
        }
    }
}