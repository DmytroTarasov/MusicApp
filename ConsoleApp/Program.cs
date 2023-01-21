// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;
using ServicesImpl;

PlayListService service = new PlayListService(new HtmlWeb { UserAgent = "Chrome/109.0.5414.74" });

// service.GetAllPlayLists();

service.GetAllPlayLists("/popular/playlists");