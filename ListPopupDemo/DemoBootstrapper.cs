using System.Windows;

namespace ListPopupDemo
{
    public class DemoBootstrapper : Caliburn.Micro.BootstrapperBase
    {
        public DemoBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
