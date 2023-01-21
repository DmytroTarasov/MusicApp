using System.Collections.ObjectModel;
using System.Linq;
using Models;
using Services;

namespace MyAvalonia.ViewModels;

public class PlayListViewModel : ViewModelBase
{
    public ObservableCollection<PlayListModel> PlayLists { get; }
    private IPlayListService _playListService;
    public PlayListViewModel(IPlayListService playListService)
    {
        _playListService = playListService;
        PlayLists = new ObservableCollection<PlayListModel>(
            _playListService.GetAllPlayLists("popular/playlists").ToList()
        );
    }
}