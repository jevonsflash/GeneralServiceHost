using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace GeneralServiceHost.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        public IndexPageViewModel IndexPage => Ioc.Default.GetRequiredService<IndexPageViewModel>();
        public AddJobWindowViewModel AddJobWindow => Ioc.Default.GetRequiredService<AddJobWindowViewModel>();



        public static void Cleanup<T>() where T : ObservableObject
        {

        }
    }
}