using Avalonia.Controls;

namespace Prism.Navigation.Avalonia.Xaml;

public class Page : UserControl
{
    public INavigation Navigation { get; }

    public Page()
    {
        Navigation = new InternalNavigation(this);
    }
    
    private class InternalNavigation : INavigation
    {
        private Page _page;
        private readonly List<Page> _modalStack = new List<Page>();
        private readonly List<Page> _navigationStack = new List<Page>();

        public IReadOnlyList<Page> ModalStack => _modalStack;
        public IReadOnlyList<Page> NavigationStack => _navigationStack;

        public InternalNavigation(Page page) => _page = page;
        
        public void InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        public async Task<Page> PopAsync()
        {
            _navigationStack.RemoveAt(_navigationStack.Count - 1);
            Page page = _navigationStack.Last();
            _page.Content = page;
            return page;
        }

        public Task<Page> PopAsync(bool animated)
        {
            return PopAsync();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public async Task PopToRootAsync()
        {
            _navigationStack.Clear();
            _page.Content = null;
        }

        public Task PopToRootAsync(bool animated) => PopToRootAsync();

        public async Task PushAsync(Page page)
        {
            _navigationStack.Add(page);
            _page.Content = page;
        }

        public Task PushAsync(Page page, bool animated) => PushAsync(page);

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        public void RemovePage(Page page)
        {
            _navigationStack.Remove(page);
            if (page == _page.Content)
                _page.Content = _navigationStack.Last();
        }
    }
}

public class ContentPage : Page
{
}

public interface INavigation
{
    IReadOnlyList<Page> ModalStack { get; }

    IReadOnlyList<Page> NavigationStack { get; }

    void InsertPageBefore(Page page, Page before);
    Task<Page> PopAsync();
    Task<Page> PopAsync(bool animated);
    Task<Page> PopModalAsync();
    Task<Page> PopModalAsync(bool animated);
    Task PopToRootAsync();
    Task PopToRootAsync(bool animated);

    Task PushAsync(Page page);

    Task PushAsync(Page page, bool animated);
    Task PushModalAsync(Page page);
    Task PushModalAsync(Page page, bool animated);

    void RemovePage(Page page);
}

public class NavigationPage : Page
{
    public Page RootPage { get; set; }
    public Page CurrentPage { get; set; }
}

// public class FlyoutPage : Page
// {
//     public Page Flyout { get; set; }
//     public Page Detail { get; set; }
// }
// public class TabbedPage : Page { }
// public class CarouselPage : Page { }