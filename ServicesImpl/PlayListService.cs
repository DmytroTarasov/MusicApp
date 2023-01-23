using Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Services;

namespace ServicesImpl;

public class PlayListService : IPlayListService
{
    private readonly HtmlWeb _web;
    public PlayListService(HtmlWeb web)
    {
        _web = web;
    }
    public IEnumerable<PlayListModel> GetAllPlayLists(string url)
    {
        HtmlDocument document = _web.Load(url);
        
        return document.DocumentNode.Descendants("music-vertical-item")
            .Select(i =>
            {
                string name = i.QuerySelector("music-link").Attributes["title"].Value.Replace("amp;", "");
                string avatar = i.QuerySelector("music-image").Attributes["src"].Value;
                string id = i.QuerySelector("music-link a").Attributes["href"].Value.Split("/").Last();
                
                return new PlayListModel { Id = id, Name = name, Avatar = avatar };
            });
    }
    public PlayListModel GetPlayListWithSongs(string url)
    {
        bool isAlbum = url.Contains("albums");
        HtmlDocument document = _web.Load(url);
        
        string playListName = document.DocumentNode.Descendants("h1").Last().Attributes["title"].Value
            .Replace("amp;", "");
        string playListDescription = document.DocumentNode.QuerySelector("music-link.primary").Attributes["title"].Value
            .Replace("amp;", "");
        string playListAvatar = document.DocumentNode.QuerySelector(".image-container>music-image").Attributes["src"].Value;

        IEnumerable<SongModel> songs = document.DocumentNode.QuerySelectorAll("music-image-row, music-text-row")
            .Where(node => !node.HasClass("disabled-hover"))
            .Select(musicNode =>
            {
                List<string> songData = musicNode.QuerySelectorAll(".content>div>music-link")
                    .Select(item => item.Attributes["title"].Value.Replace("amp;", "")).ToList();

                return new SongModel
                {
                    Name = songData.ElementAt(0), 
                    Artist = isAlbum ? playListDescription : songData.ElementAt(1), 
                    Album = isAlbum ? playListName : songData.ElementAt(2), 
                    Duration = isAlbum ? songData.ElementAt(1) : songData.ElementAt(3)
                };
            });
        return new PlayListModel { Name = playListName, Description = playListDescription, Avatar = playListAvatar, Songs = songs };
    }
}