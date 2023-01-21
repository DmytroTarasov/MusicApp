using Models;

namespace Services;

public interface IPlayListService
{
    IEnumerable<PlayListModel> GetAllPlayLists();
    PlayListModel GetPlayListWithSongs();
}