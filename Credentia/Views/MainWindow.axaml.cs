using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;
using Avalonia.Markup.Xaml;
using Credentia.ViewModels;

namespace Credentia.Views;


public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}



