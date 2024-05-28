using Pipopolam.Avalonia.NavigationPages;
using Prism.Navigation.Avalonia.Xaml;

namespace Prism.Common
{
    /// <summary>
    /// Interface to signify that a class must have knowledge of a specific <see cref="Pipopolam.Avalonia.NavigationPages.Page"/> instance in order to function properly.
    /// </summary>
    public interface IPageAware
    {
        /// <summary>
        /// The <see cref="Pipopolam.Avalonia.NavigationPages.Page"/> instance.
        /// </summary>
        Page Page { get; set; }
    }
}
