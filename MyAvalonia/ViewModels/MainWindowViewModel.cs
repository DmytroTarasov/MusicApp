using System.Reactive;
using Models;
using ReactiveUI;
using Services;

namespace MyAvalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PlayListViewModel _playListViewModel;
    private readonly PlayListDetailsViewModel _playListDetailsViewModel;
    private readonly IPlayListService _playListService;
    public MainWindowViewModel(IPlayListService playListService, PlayListViewModel playListViewModel, 
        PlayListDetailsViewModel playListDetailsViewModel)
    {
        _playListViewModel = playListViewModel;
        _playListDetailsViewModel = playListDetailsViewModel;
        _playListService = playListService;
        
        LoadPlayListView();
        
        LoadPlayListViewCommand = ReactiveCommand.Create(LoadPlayListView);
        LoadPlayListDetailsViewCommand = ReactiveCommand.Create<string>(LoadPlayListDetailsView);
    }
    public ReactiveCommand<Unit, Unit> LoadPlayListViewCommand { get; }
    public ReactiveCommand<string, Unit> LoadPlayListDetailsViewCommand { get; }
    
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentViewModel, value);
        }
    }
    private void LoadPlayListView()
    {
        CurrentViewModel = _playListViewModel;
    }
    private void LoadPlayListDetailsView(string id)
    {
        SelectedPlayList = _playListService.GetPlayListWithSongs("playlists/" + id);
        CurrentViewModel = _playListDetailsViewModel;
    }
    
    private PlayListModel _selectedPlayList;
    public PlayListModel SelectedPlayList
    {
        get => _selectedPlayList;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedPlayList, value);
        }
    }
}