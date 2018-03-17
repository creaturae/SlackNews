using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace SlackNews
{

    /// <summary>
    /// Referencias:
    /// http://stackoverflow.com/a/24233127
    /// Ajuda:
    /// http://json2csharp.com
    /// https://rss2json.com
    /// </summary>
    class Program
    {
        public class Feed
        {
            public string url { get; set; }
            public string title { get; set; }
            public string link { get; set; }
            public string author { get; set; }
            public string description { get; set; }
            public string image { get; set; }
        }

        public class Enclosure
        {
            public string link { get; set; }
        }

        public class Item
        {
            public string title { get; set; }
            public string pubDate { get; set; }
            public string link { get; set; }
            public string guid { get; set; }
            public string author { get; set; }
            public string thumbnail { get; set; }
            public string description { get; set; }
            public string content { get; set; }
            public Enclosure enclosure { get; set; }
            public List<string> categories { get; set; }
        }

        public class RootObject
        {
            public string status { get; set; }
            public Feed feed { get; set; }
            public List<Item> items { get; set; }
        }



        public static void TestPostMessage()
        {
            var json = "";

            using (WebClient webClient = new System.Net.WebClient())
            {
                WebClient n = new WebClient();
                json = n.DownloadString("https://api.rss2json.com/v1/api.json?rss_url=http%3A%2F%2Fpox.globo.com%2Frss%2Fg1%2F&api_key=jakdinwrpoqnr7vm7rqrjehyslmgi6wnd5thc6co&order_by=pubDate&count=1");
                string valueOriginal = Convert.ToString(json);
                //Console.WriteLine(json);
            }

            var obj = JsonConvert.DeserializeObject<RootObject>(json);
            string post = obj.items[0].title + ":" + obj.items[0].description ;

            // Pegar acess token no slack.com
            string urlWithAccessToken = "https://hooks.slack.com/services/xxxxxx/xxxxxxxxxxxxx";

            SlackClient client = new SlackClient(urlWithAccessToken);

            
            client.PostMessage(username: "Mauricio",
                       text: post,
                       channel: "#noticias");
                       
        }

        static void Main(string[] args)
        {

            TestPostMessage();

        }
    }
}
