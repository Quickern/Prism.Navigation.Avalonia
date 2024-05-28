using Prism.Navigation.Avalonia.Xaml;

namespace Prism.Common
{
    /// <summary>
    /// Interface to signify that a class must have knowledge of a specific <see cref="Navigation.Avalonia.Xaml.Page"/> instance in order to function properly.
    /// </summary>
    public interface IPageAware
    {
        /// <summary>
        /// The <see cref="Navigation.Avalonia.Xaml.Page"/> instance.
        /// </summary>
        Page Page { get; set; }
    }
}
