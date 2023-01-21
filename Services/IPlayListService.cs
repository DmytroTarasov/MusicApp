using Models;

namespace Services;

public interface IPlayListService
{
    IEnumerable<PlayListModel> GetAllPlayLists(string url);
    PlayListModel GetPlayListWithSongs(string url);
}