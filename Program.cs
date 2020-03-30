using System;
using System.Threading.Tasks;
using RestSharp;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace restsharp_apicall_console
{
    public class MetaData
    {
        public string Num_Results { get; set;}
    }
    public class Article
    {
        public string Section { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public string Byline { get; set; }

        public List<Multimedia> Multimedia { get; set; }
    }

    public class Multimedia
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Caption { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apiCallTask = ApiHelper.ApiCall(EnvironmentVariables.ApiKey);
            var result = apiCallTask.Result;
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);

            // get the Num_Results
            // MetaData metaData = JsonConvert.DeserializeObject<MetaData>(jsonResponse.ToString());

            // get Multimedia information within one result
            // List<Multimedia> multimediaList = JsonConvert.DeserializeObject<List<Multimedia>>(jsonResponse["results"][0]["multimedia"].ToString());

            // get all articles in results array
            List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString())
            ;

            foreach (Article article in articleList)
            {
                Console.WriteLine($"Section: {article.Section}");
                Console.WriteLine($"Title: {article.Title}");
                Console.WriteLine($"Abstract: {article.Abstract}");
                Console.WriteLine($"Url: {article.Url}");
                Console.WriteLine($"Byline: {article.Byline}");
                // foreach (Multimedia media in article.Multimedia)
                // {
                //     Console.WriteLine("---------");
                //     Console.WriteLine($"MULTIMEDIA");
                //     Console.WriteLine($"Type: {media.Type}");
                //     Console.WriteLine($"SubType: {media.SubType}");
                //     Console.WriteLine($"Caption: {media.Caption}");
                // }
                Console.WriteLine("__________________________________");
            }

            // foreach (Multimedia media in multimediaList)
            // {
            //     Console.WriteLine("---------");
            //     Console.WriteLine($"MULTIMEDIA");
            //     Console.WriteLine($"Type: {media.Type}");
            //     Console.WriteLine($"SubType: {media.SubType}");
            //     Console.WriteLine($"Caption: {media.Caption}");
            // }

            // Console.WriteLine($"Num_results: {metaData.Num_Results}");
        }

        class ApiHelper
        {

            public static async Task<string> ApiCall(string apiKey)
            {
                RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
                RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
                var response = await client.ExecuteTaskAsync(request);
                return response.Content;
            }
        }
    }
}
