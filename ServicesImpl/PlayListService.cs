using Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Services;

namespace ServicesImpl;

public class PlayListService : IPlayListService
{
    private HtmlDocument document;
    public PlayListService()
    {
        // var url = "https://music.amazon.com/popular/playlists";
        var url = "https://music.amazon.com/playlists/B083NH7LZK";
        var web = new HtmlWeb
        {
            UserAgent = "Chrome/109.0.5414.74"
        };
        document = web.Load(url);
    }
    
    public IEnumerable<PlayListModel> GetAllPlayLists()
    {
        IEnumerable<PlayListModel> list = document.DocumentNode.Descendants("music-vertical-item")
            .Select(i =>
            {
                string name = i.Descendants("music-link").First().Attributes["title"].Value;
                string avatar = i.Descendants("music-image").First().Attributes["src"].Value;

                return new PlayListModel { Name = name, Avatar = avatar };
            });

        return list;
    }
    public PlayListModel GetPlayListWithSongs()
    {
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