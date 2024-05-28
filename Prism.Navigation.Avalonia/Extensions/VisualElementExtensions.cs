using Avalonia;
using Pipopolam.Avalonia.NavigationPages;
using Prism.Navigation.Avalonia.Xaml;

namespace Prism.Extensions
{
    internal static class VisualElementExtensions
    {
        public static bool TryGetParentPage(this Visual element, out Page page)
        {
            page = GetParentPage(element);
            return page != null;
        }

        private static Page GetParentPage(StyledElement visualElement)
        {
            switch (visualElement.Parent)
            {
                case Page page:
                    return page;
                case null:
                    return null;
                default:
                    return GetParentPage(visualElement.Parent);
            }
        }
    }
}
