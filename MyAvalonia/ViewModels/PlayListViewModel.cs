using System.Collections.Generic;
using Models;
using Services;

namespace MyAvalonia.ViewModels;

public class PlayListViewModel : ViewModelBase
{
    public IEnumerable<PlayListModel> PlayLists { get; }
    private IPlayListService _playListService;
    public PlayListViewModel(IPlayListService playListService)
    {
        _playListService = playListService;
        PlayLists = _playListService.GetAllPlayLists("popular/playlists");
    }
}