namespace Pipopolam.Avalonia.NavigationPages;

public class NavigationPage : Page
{
    public Page? RootPage { get; set; }
    public Page? CurrentPage => Content as Page;

    public NavigationPage()
    {
        Navigation = new NavigationImpl(this);
    }
    
    private class NavigationImpl : INavigation
    {
        private readonly NavigationPage _page;
        
        private readonly List<Page> _modalStack = new List<Page>();
        private readonly List<Page> _navigationStack = new List<Page>();

        public IReadOnlyList<Page> ModalStack => _modalStack;
        public IReadOnlyList<Page> NavigationStack => _navigationStack;

        public NavigationImpl(NavigationPage page) => _page = page;
        
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
