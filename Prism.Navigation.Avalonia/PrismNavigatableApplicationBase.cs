using Avalonia;
using Pipopolam.Avalonia.NavigationPages;
using Prism.Common;
using Prism.Extensions;
using Prism.Navigation.Avalonia.Xaml;

namespace Prism;

public abstract class PrismNavigatableApplicationBase : PrismApplicationBase
{
    public new static PrismNavigatableApplicationBase Current => (PrismNavigatableApplicationBase) Application.Current;
    
    public Page MainPage
    {
        get => ((MainWindow)MainWindow).MainPage;
        set => ((MainWindow)MainWindow).MainPage = value;
    }

    public const string NavigationServiceName = "PageNavigationService";
    
    protected INavigationService NavigationService { get; set; }
    
    protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
    {
        base.RegisterRequiredTypes(containerRegistry);

        containerRegistry.RegisterSingleton<IApplicationProvider, ApplicationProvider>();
        
        containerRegistry.TryRegisterScoped<INavigationService, PageNavigationService>();
        containerRegistry.Register<INavigationService, PageNavigationService>(NavigationServiceName);
        
        containerRegistry.Register<MainWindow>();
    }

    // protected override void ConfigureViewModelLocator()
    // {
        // ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) =>
        // {
        //     List<(Type Type, object Instance)> overrides = [];
        //     if (Container.IsRegistered<IResolverOverridesHelper>())
        //     {
        //         var resolver = Container.Resolve<IResolverOverridesHelper>();
        //         var resolverOverrides = resolver.GetOverrides();
        //         if (resolverOverrides.Any())
        //             overrides.AddRange(resolverOverrides);
        //     }
        //
        //     if (!overrides.Any(x => x.Type == typeof(INavigationService)))
        //     {
        //         var navService = CreateNavigationService(view);
        //         overrides.Add((typeof(INavigationService), navService));
        //     }
        //
        //     return Container.Resolve(type, overrides.ToArray());
        // });
    // }
    
    private INavigationService CreateNavigationService(object view)
    {
        if (view is Page page)
        {
            return Navigation.Xaml.Navigation.GetNavigationService(page);
        }
        else if (view is Visual visualElement && visualElement.TryGetParentPage(out var parent))
        {
            return Navigation.Xaml.Navigation.GetNavigationService(parent);
        }

        return Container.Resolve<INavigationService>();
    }

    protected override AvaloniaObject CreateShell()
    {
        MainWindow window = Container.Resolve<MainWindow>();
        NavigationService = CreateNavigationService(window.MainPage);
        return window;
    }
}