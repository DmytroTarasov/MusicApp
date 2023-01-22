using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using MyAvalonia.ViewModels;
using MyAvalonia.Views;
using Services;
using ServicesImpl;

namespace MyAvalonia;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPlayListService, PlayListService>();
        services.AddSingleton<HtmlWeb, HtmlWeb>(provider => new HtmlWeb { UserAgent = "Chrome/109.0.5414.74" });
        
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<PlayListViewModel>();
        services.AddTransient<PlayListDetailsViewModel>();

        // services.AddSingleton<MainWindow>();
    }
}