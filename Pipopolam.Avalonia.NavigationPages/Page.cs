using Avalonia.Controls;

namespace Pipopolam.Avalonia.NavigationPages;

public class Page : UserControl
{
    public INavigation Navigation { get; protected set; }

    public Page()
    {
        Navigation = new NavigationProxy(this);
    }

    protected class NavigationProxy : INavigation
    {
        private readonly Page? _parentPage;

        public NavigationProxy(Page page) => _parentPage = page.Parent as Page;

        public IReadOnlyList<Page> ModalStack => _parentPage?.Navigation.ModalStack;
        public IReadOnlyList<Page> NavigationStack => _parentPage?.Navigation.NavigationStack;
        public void InsertPageBefore(Page page, Page before) => _parentPage?.Navigation.InsertPageBefore(page, before);

        public Task<Page> PopAsync() => _parentPage?.Navigation.PopAsync();

        public Task<Page> PopAsync(bool animated) => _parentPage?.Navigation.PopAsync(animated);

        public Task<Page> PopModalAsync() => _parentPage?.Navigation.PopModalAsync();

        public Task<Page> PopModalAsync(bool animated) => _parentPage?.Navigation.PopModalAsync(animated);

        public Task PopToRootAsync() => _parentPage?.Navigation.PopToRootAsync();

        public Task PopToRootAsync(bool animated) => _parentPage?.Navigation.PopToRootAsync(animated);

        public Task PushAsync(Page page) => _parentPage?.Navigation.PushAsync(page);

        public Task PushAsync(Page page, bool animated) => _parentPage?.Navigation.PushAsync(page, animated);

        public Task PushModalAsync(Page page) => _parentPage?.Navigation.PushModalAsync(page);

        public Task PushModalAsync(Page page, bool animated) => _parentPage?.Navigation.PushModalAsync(page, animated);

        public void RemovePage(Page page) => _parentPage?.Navigation.RemovePage(page);
    }
}
