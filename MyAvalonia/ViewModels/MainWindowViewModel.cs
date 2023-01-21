using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace MyAvalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PlayListViewModel _playListViewModel;
    public MainWindowViewModel(PlayListViewModel playListViewModel)
    {
        _playListViewModel = playListViewModel;
        
        LoadPlayListView();
        
        LoadPlayListViewCommand = ReactiveCommand.Create(LoadPlayListView);
    }
    
    // public ICommand LoadPlayListViewCommand { get; }
    
    public ReactiveCommand<Unit, Unit> LoadPlayListViewCommand { get; }
    
    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
        }
    }
    private void LoadPlayListView()
    {
        CurrentViewModel = _playListViewModel;
    }
    
}