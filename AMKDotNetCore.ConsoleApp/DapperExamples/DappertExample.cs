using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using AMKDotNetCore.ConsoleApp.Models;


namespace AMKDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AHMTZDotNetCore",
            UserID = "sa",
            Password = "sa@123"
        };

        public void Run()
        {
            Update(3,"title3", "author3", "content3");
            Delete(4);
            Read();
            Edit(1);
            Edit(2);
        }

        private void Read()
        {
            string query = "select * from tbl_blog order by Blog_Id desc";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            List<BlogDataModel> lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }

            //var lst = db.Query(query).ToList();
            //foreach (var item in lst)
            //{
            //    Console.WriteLine(item.Blog_Id);
            //    Console.WriteLine(item.Blog_Title);
            //    Console.WriteLine(item.Blog_Autthor);
            //    Console.WriteLine(item.Blog_Content);
            //}
        }
        
        private void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                     SET [Blog_Title] = @Blog_Title,
                         [Blog_Author] = @Blog_Author,
                         [Blog_Content] = @Blog_Content
                     WHERE [Blog_Id] = @Blog_Id";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
                Blog_Author = author,
                Blog_Content = content,
                Blog_Title = title,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            string query = $@"DELETE FROM [dbo].[Tbl_Blog]
                     WHERE [Blog_Id] = @Blog_Id"; 

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id,
               
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";

            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            string query = "select * from tbl_blog where Blog_Id = @Blog_Id;";

            BlogDataModel blog = new BlogDataModel()
            {
                Blog_Id = id
            };
            IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            BlogDataModel item = db.Query<BlogDataModel>(query, blog).FirstOrDefault();

            //if (item is null)
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
    }
}
