using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text;

namespace AMKDotNetCore.ConsoleApp.AdoDotNetExamples
{
    // private
    // public
    // protected
    // internal
    public class AdoDotNetExample
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
            
            Delete(1);
            Read();
            
          
        }

        private void Read()
        {
            string query = "select * from tbl_blog";
            
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query,connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_Id"].ToString());
                Console.WriteLine(dr["Blog_Title"].ToString());
                Console.WriteLine(dr["Blog_Author"].ToString());
                Console.WriteLine(dr["Blog_Content"].ToString());
            }


        }
        private void Read(int pageNo, int pageSize = 10)
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            string query = @"select * from tbl_blog 
order by Blog_Id desc 
OFFSET @SkipRowCount ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SkipRowCount", skipRowCount);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_Id"].ToString());
                Console.WriteLine(dr["Blog_Title"].ToString());
                Console.WriteLine(dr["Blog_Author"].ToString());
                Console.WriteLine(dr["Blog_Content"].ToString());
            }
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

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            connection.Close();

            Console.WriteLine(message);
        }

        private void Update(int id,string title, string author, string content) {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                     SET [Blog_Title] = @Blog_Title,
                         [Blog_Author] = @Blog_Author,
                         [Blog_Content] = @Blog_Content
                     WHERE [Blog_Id] = @Blog_Id"; 
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id",id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            string query = $@"DELETE FROM[dbo].[Tbl_Blog] WHERE[Blog_ID] = @Blog_ID";
            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";

            connection.Close();

            Console.WriteLine(message);
        }
        private void Edit(int id)
        {
            string query = "select * from tbl_blog where Blog_Id = @Blog_Id;";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine(" data found.");

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["Blog_Id"].ToString());
            Console.WriteLine(dr["Blog_Title"].ToString());
            Console.WriteLine(dr["Blog_Author"].ToString());
            Console.WriteLine(dr["Blog_Content"].ToString());
        }




    }
}
