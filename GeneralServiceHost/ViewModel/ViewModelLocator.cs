using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace GeneralServiceHost.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<IndexPageViewModel>();
            SimpleIoc.Default.Register<AddJobWindowViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public IndexPageViewModel IndexPage => ServiceLocator.Current.GetInstance<IndexPageViewModel>();
        public AddJobWindowViewModel AddJobWindow => ServiceLocator.Current.GetInstance<AddJobWindowViewModel>();


        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<AddJobWindowViewModel>();
            SimpleIoc.Default.Register<AddJobWindowViewModel>();
        }
    }
}