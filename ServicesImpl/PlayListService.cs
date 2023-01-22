using Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Services;

namespace ServicesImpl;

public class PlayListService : IPlayListService
{
    private const string BaseUrl = "https://music.amazon.com/";
    private readonly HtmlWeb _web;
    public PlayListService(HtmlWeb web)
    {
        _web = web;
    }
    public IEnumerable<PlayListModel> GetAllPlayLists(string url)
    {
        HtmlDocument document = _web.Load(BaseUrl + url);
        
        return document.DocumentNode.Descendants("music-vertical-item")
            .Select(i =>
            {
                string name = i.QuerySelector("music-link").Attributes["title"].Value;
                string avatar = i.QuerySelector("music-image").Attributes["src"].Value;
                string id = i.QuerySelector("music-link a").Attributes["href"].Value.Split("/").Last();
                
                return new PlayListModel { Id = id, Name = name, Avatar = avatar };
            });
    }
    public PlayListModel GetPlayListWithSongs(string url)
    {
        HtmlDocument document = _web.Load(BaseUrl + url);
        
        string playListName = document.DocumentNode.Descendants("h1").Last().Attributes["title"].Value;
        string playListDescription = document.DocumentNode.QuerySelector("music-link.secondary").Attributes["title"].Value;
        string playListAvatar = document.DocumentNode.QuerySelector(".image-container>music-image").Attributes["src"].Value;

        IEnumerable<SongModel> songs = document.DocumentNode.QuerySelectorAll("music-image-row")
            .Where(node => !node.HasClass("disabled-hover"))
            .Select(musicNode =>
            {
                List<string> songData = musicNode.QuerySelectorAll(".content>div>music-link")
                    .Select(item => item.Attributes["title"].Value).ToList();

                return new SongModel
                {
                    Name = songData.ElementAt(0), 
                    Artist = songData.ElementAt(1), 
                    Album = songData.ElementAt(2), 
                    Duration = songData.ElementAt(3)
                };
            });
        return new PlayListModel { Name = playListName, Description = playListDescription, Avatar = playListAvatar, Songs = songs };
    }
}