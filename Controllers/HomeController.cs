
using CloudApp.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Activity = System.Diagnostics.Activity;

namespace CloudApp.Controllers
{
    public class HomeController : Controller
    {
        public List<Videos> videos { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            // ViewData["Message"] = "Your contact page.";
            // //AIzaSyBlRGD66c7mgfhm - 8CuVT48IzWVKgRSy - U


            //   var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            //     {
            //         ApiKey = "AIzaSyC1GyMmF8ZEDwuVMaCGunE3mdL2VCo0b3E",
            //         ApplicationName = this.GetType().ToString()
            //     });

            //     var searchListRequest = youtubeService.Search.List("snippet");
            //     searchListRequest.Q = "Honey Singh"; // Replace with your search term.
            //     searchListRequest.MaxResults = 10;

            //     // Call the search.list method to retrieve results matching the specified query term.
            //     var searchListResponse = await searchListRequest.ExecuteAsync();

            // //    Dictionary<string, string> videos = new Dictionary<string, string>();
            // Dictionary<string, string> channels = new Dictionary<string, string>();
            // Dictionary<string, string> playlists = new Dictionary<string, string>();

            //  this.videos = new List<Videos>();


            // // Add each result to the appropriate list, and then display the lists of
            // // matching videos, channels, and playlists.
            // foreach (var searchResult in searchListResponse.Items)
            //     {

            //     switch (searchResult.Id.Kind)
            //         {
            //             case "youtube#video":
            //             videos.Add(new Videos()
            //             { Title = searchResult.Snippet.Title, VideoID = "https://www.youtube.com/embed/"+searchResult.Id.VideoId });
            //                 break;

            //             case "youtube#channel":
            //                 channels.Add(searchResult.Snippet.Title, searchResult.Id.ChannelId);
            //                 break;

            //             case "youtube#playlist":
            //                 playlists.Add(searchResult.Snippet.Title, searchResult.Id.PlaylistId);
            //                 break;
            //         }
            //     }

            // //Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
            // //Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
            // //Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
            //// ViewBag.IframeUrl = "https://www.youtube.com/embed/" +videos["Loca"].Contains("Loca");
            this.videos = new List<Videos>();
            return View(this.videos);
        }


        [HttpPost]
        public async Task<IActionResult> Search(string search, string minimumShow)
        {
            videos = new List<Videos>();
            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "AIzaSyAeMYqvSVNis-YlKtBwTmVYExOhQ-WMCjA",
                    ApplicationName = this.GetType().ToString()
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = search; // Replace with your search term.
                searchListRequest.MaxResults = Convert.ToInt32(minimumShow);

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = await searchListRequest.ExecuteAsync();

                //    Dictionary<string, string> videos = new Dictionary<string, string>();
                Dictionary<string, string> channels = new Dictionary<string, string>();
                Dictionary<string, string> playlists = new Dictionary<string, string>();

                this.videos = new List<Videos>();


                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                foreach (var searchResult in searchListResponse.Items)
                {

                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            videos.Add(new Videos()
                            {
                                Title = searchResult.Snippet.Title,
                                VideoID = "https://www.youtube.com/embed/" + searchResult.Id.VideoId

                            });
                            break;

                        case "youtube#channel":
                            channels.Add(searchResult.Snippet.Title, searchResult.Id.ChannelId);
                            break;

                        case "youtube#playlist":
                            playlists.Add(searchResult.Snippet.Title, searchResult.Id.PlaylistId);
                            break;
                    }
                }

                //Console.WriteLine(String.Format("Videos:\n{0}\n", string.Join("\n", videos)));
                //Console.WriteLine(String.Format("Channels:\n{0}\n", string.Join("\n", channels)));
                //Console.WriteLine(String.Format("Playlists:\n{0}\n", string.Join("\n", playlists)));
                // ViewBag.IframeUrl = "https://www.youtube.com/embed/" +videos["Loca"].Contains("Loca");
            }
            catch
            {


            }



            return View("Contact", videos);



        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
