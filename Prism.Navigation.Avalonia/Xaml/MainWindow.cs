using Avalonia.Controls;

namespace Prism.Navigation.Avalonia.Xaml;

public class MainWindow : Window
{
    public Page MainPage
    {
        get => Content as Page;
        set => Content = value;
    }
}
