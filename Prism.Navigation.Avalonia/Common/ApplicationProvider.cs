using Avalonia;
using Prism.Navigation.Avalonia.Xaml;

namespace Prism.Common
{
    /// <summary>
    /// Provides Application components.
    /// </summary>
    public class ApplicationProvider : IApplicationProvider
    {
        /// <inheritdoc/>
        public Page MainPage
        {
            get => PrismNavigatableApplicationBase.Current?.MainPage;
            set => PrismNavigatableApplicationBase.Current.MainPage = value;
        }
    }
}
