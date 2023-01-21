namespace Models;

public class PlayListModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Avatar { get; set; }
    public string Description { get; set; }
    public IEnumerable<SongModel> Songs { get; set; }
}