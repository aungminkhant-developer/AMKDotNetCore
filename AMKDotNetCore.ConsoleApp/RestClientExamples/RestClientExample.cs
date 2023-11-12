using AMKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AMKDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        public async Task Run()
        {

            await Read();
            await Edit(9);
            await Create("title", "author", "content");

        }

        private async Task Read()
        {
            RestClient client = new RestClient();  
            RestRequest request = new RestRequest("https://localhost:7180/api/blog", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
           
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
                // Joson to C# Object
                BlogListResponseModel model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }
        }

        private async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title,
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7180/api/blog", Method.Post);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }

        }


        private async Task Update(int id, string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title,
            };
            string blogJson = JsonConvert.SerializeObject(blog);
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7180/api/blog/{id}", Method.Put);
            request.AddBody(blog);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }

        }
        private async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7180/api/blog/{id}", Method.Delete);
            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }
        }

        private async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7180/api/blog/{id}", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content;
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }

        }
    }
}
