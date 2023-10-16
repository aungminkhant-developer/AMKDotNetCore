using AMKDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        public void Run()
        {
            //Create( "title3", "author3", "content3");
            //Read();
            Edit(17);
            Edit(19);
        }
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.OrderByDescending(x=> x.Blog_Id).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }
        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = author, 
                Blog_Content = content,
                Blog_Title = title
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed";

            Console.WriteLine(message);
         }
        private void Edit (int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);

        }
        private void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            item.Blog_Title = title; 
            item.Blog_Author = author;
            item.Blog_Content = content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed";

            Console.WriteLine(message);
        }
        private void Delete (int id)
        {
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";

            Console.WriteLine(message);
        }
    }

}
