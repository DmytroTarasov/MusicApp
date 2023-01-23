using System;
using System.Reactive;
using Models;
using ReactiveUI;
using Services;

namespace MyAvalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PlayListDetailsViewModel _playListDetailsViewModel;
    private readonly IPlayListService _playListService;
    public MainWindowViewModel(IPlayListService playListService, PlayListDetailsViewModel playListDetailsViewModel)
    {
        _playListDetailsViewModel = playListDetailsViewModel;
        _playListService = playListService;
        
        LoadPlayListCommand = ReactiveCommand.Create<string>(LoadPlayList);
    }
    public ReactiveCommand<string, Unit> LoadPlayListCommand { get; }
    
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }
    }
    private void LoadPlayList(string url)
    {
        PlayList = _playListService.GetPlayListWithSongs(url);
        CurrentViewModel = _playListDetailsViewModel;
    }

    private PlayListModel _playList;
    public PlayListModel PlayList
    {
        get => _playList;
        set
        {
            this.RaiseAndSetIfChanged(ref _playList, value);
        }
    }
}