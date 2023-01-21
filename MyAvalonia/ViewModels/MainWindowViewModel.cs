using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using Models;
using Services;

namespace MyAvalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    // public string Greeting => "Welcome to Avalonia!";
    public ObservableCollection<PlayListModel> PlayLists { get; }
    private IPlayListService _playListService;

    // public MainWindowViewModel() { }
    
    public MainWindowViewModel(IPlayListService playListService)
    {
        _playListService = playListService;
        PlayLists = new ObservableCollection<PlayListModel>(
            _playListService.GetAllPlayLists().ToList()
        );
    }
}