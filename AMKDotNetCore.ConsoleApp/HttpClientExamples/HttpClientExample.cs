using AMKDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMKDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            
           await Read();
           await Edit(9);
           await Create("title","author","content");
            
        }

        private async Task Read()
        {
             HttpClient client = new HttpClient();
             var response = await client.GetAsync("https://localhost:7180/api/blog");
             if (response.IsSuccessStatusCode)
            {
                string jsonStr =await response.Content.ReadAsStringAsync();
                // Joson to C# Object
                BlogListResponseModel model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model,Formatting.Indented));
                
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
            string blogJson = JsonConvert.SerializeObject(blog);
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://localhost:7180/api/blog", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
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
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PutAsync("https://localhost:7180/api/blog/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }

        }
        private async Task Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync("https://localhost:7180/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }
        }

        private async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7180/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                // Joson to C# Object
                BlogResponseModel model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model));
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));

            }

        } 
    }
}
