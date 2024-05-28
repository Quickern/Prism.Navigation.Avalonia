using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Pipopolam.Avalonia.NavigationPages;
using Prism.Common;

namespace Prism.Navigation.Xaml
{
    /// <summary>
    /// Provides Attachable properties for Navigation
    /// </summary>
    public static class Navigation
    {
        static Navigation()
        {
            NavigationScopeProperty.Changed.Subscribe(args => OnNavigationScopeChanged(args.Sender, args.OldValue.GetValueOrDefault(), args.NewValue.GetValueOrDefault()));
            CanNavigateProperty.Changed.Subscribe(args => OnCanNavigatePropertyChanged(args.Sender, args.OldValue.GetValueOrDefault(), args.NewValue.GetValueOrDefault()));
        }

        internal static readonly AttachedProperty<INavigationService> NavigationServiceProperty =
            AvaloniaProperty.RegisterAttached<Control, INavigationService>("NavigationService", typeof(Navigation));

        internal static readonly AttachedProperty<IScopedProvider> NavigationScopeProperty =
            AvaloniaProperty.RegisterAttached<Control, IScopedProvider>("NavigationScope", typeof(Navigation));

        private static void OnNavigationScopeChanged(AvaloniaObject bindable, IScopedProvider oldValue, IScopedProvider newValue)
        {
            if (oldValue == newValue)
            {
                return;
            }

            if (oldValue != null && newValue is null)
            {
                oldValue.Dispose();
                return;
            }

            if (newValue != null)
            {
                newValue.IsAttached = true;
            }
        }

        /// <summary>
        /// Provides bindable CanNavigate Bindable Property
        /// </summary>
        public static readonly AttachedProperty<bool> CanNavigateProperty =
            AvaloniaProperty.RegisterAttached<Control, bool>("CanNavigate", typeof(Navigation), true);

        internal static readonly AttachedProperty<Action> RaiseCanExecuteChangedInternalProperty =
            AvaloniaProperty.RegisterAttached<Control, Action>("RaiseCanExecuteChangedInternal", typeof(Navigation));

        /// <summary>
        /// Gets the Bindable Can Navigate property for an element
        /// </summary>
        /// <param name="view">The bindable element</param>
        public static bool GetCanNavigate(AvaloniaObject view) => view.GetValue(CanNavigateProperty);

        /// <summary>
        /// Sets the Bindable Can Navigate property for an element
        /// </summary>
        /// <param name="view">The bindable element</param>
        /// <param name="value">The Can Navigate value</param>
        public static void SetCanNavigate(AvaloniaObject view, bool value) => view.SetValue(CanNavigateProperty, value);

        /// <summary>
        /// Gets the instance of <see cref="INavigationService"/> for the given <see cref="Page"/>
        /// </summary>
        /// <param name="page">The <see cref="Page"/></param>
        /// <returns>The <see cref="INavigationService"/></returns>
        /// <remarks>Do not use... this is an internal use API</remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static INavigationService GetNavigationService(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            var container = ContainerLocator.Container;
            var navService = (INavigationService)page.GetValue(NavigationServiceProperty);
            if (navService is null)
            {
                var currentScope = (IScopedProvider)page.GetValue(NavigationScopeProperty) ?? container.CurrentScope;

                if (currentScope is null)
                    currentScope = container.CreateScope();

                if (!currentScope.IsAttached)
                    page.SetValue(NavigationScopeProperty, currentScope);

                navService = CreateNavigationService(currentScope, page);
            }
            else if (navService is IPageAware pa && pa.Page != page)
            {
                var scope = container.CreateScope();
                page.SetValue(NavigationScopeProperty, scope);
                page.SetValue(NavigationServiceProperty, null);
                return GetNavigationService(page);
            }

            return navService;
        }

        private static INavigationService CreateNavigationService(IScopedProvider scope, Page page)
        {
            var navService = scope.Resolve<INavigationService>();
            switch (navService)
            {
                case IPageAware pa when pa.Page is null:
                    pa.Page = page;
                    break;
                case IPageAware pa1 when pa1.Page != page:
                    return CreateNavigationService(ContainerLocator.Container.CreateScope(), page);
            }

            page.SetValue(NavigationScopeProperty, scope);
            scope.IsAttached = true;
            page.SetValue(NavigationServiceProperty, navService);

            return navService;
        }

        internal static Action GetRaiseCanExecuteChangedInternal(AvaloniaObject view) => (Action)view.GetValue(RaiseCanExecuteChangedInternalProperty);

        internal static void SetRaiseCanExecuteChangedInternal(AvaloniaObject view, Action value) => view.SetValue(RaiseCanExecuteChangedInternalProperty, value);

        private static void OnCanNavigatePropertyChanged(AvaloniaObject bindable, object oldvalue, object newvalue)
        {
            var action = GetRaiseCanExecuteChangedInternal(bindable);
            action?.Invoke();
        }
    }
}
