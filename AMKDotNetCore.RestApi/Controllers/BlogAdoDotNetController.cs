using AMKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.SqlClient;


namespace AMKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public BlogAdoDotNetController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection");
            _sqlConnectionStringBuilder =new SqlConnectionStringBuilder(connectionString);
        }

        [HttpGet]
        public IActionResult GetBolgS()
        {
            string query = "select * from tbl_blog";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            List<BlogDataModel> lst = new List<BlogDataModel>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogDataModel item = new BlogDataModel()
                {
                    Blog_Id = Convert.ToInt32(dr["Blog_Id"].ToString()),
                    Blog_Title = dr["Blog_Title"].ToString(),
                    Blog_Author = dr["Blog_Author"].ToString(),
                    Blog_Content = dr["Blog_Content"].ToString(),


                };
                lst.Add(item);
            }

            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Ok(model);
        }
        [HttpGet("{id}")]
        public IActionResult EditBolg(int id)
        {
            string query = "select * from tbl_blog where Blog_Id = @Blog_Id;";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            BlogResponseModel model = new BlogResponseModel();

            if (dt.Rows.Count == 0)
            {
                model.IsSuccess=false; 
                model.Message = ("No data found.");
                return NotFound(model);

            }
            DataRow dr = dt.Rows[0];

            BlogDataModel item = new BlogDataModel() 
            {
                Blog_Id = Convert.ToInt32(dr["Blog_Id"].ToString()),
                Blog_Title = dr["Blog_Title"].ToString(),
                Blog_Author = dr["Blog_Author"].ToString(),
                Blog_Content = dr["Blog_Content"].ToString(),
            };  


            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;

            return Ok(model);
        }
        [HttpPost]
        public IActionResult CreateBlog([FromBody] BlogDataModel blog)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                ([Blog_Title]
                ,[Blog_Author]
                ,[Blog_Content])
            VALUES
                (@Blog_Title
                ,@Blog_Author
                ,@Blog_Content)
            ";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title );
            cmd.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            cmd.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            connection.Close();
           
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                Data = blog

            };
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogDataModel blog)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                     SET [Blog_Title] = @Blog_Title,
                         [Blog_Author] = @Blog_Author,
                         [Blog_Content] = @Blog_Content
                     WHERE [Blog_Id] = @Blog_Id";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", blog.Blog_Title);
            cmd.Parameters.AddWithValue("@Blog_Author", blog.Blog_Author);
            cmd.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            int result = cmd.ExecuteNonQuery();
            blog.Blog_Id = id;
            string message = result > 0 ? "Update Successful." : "Update Failed.";

            connection.Close();
            BlogResponseModel model = new BlogResponseModel();
            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                Data=blog,
            };
            return Ok(model);
        }
        //[HttpPatch("{id}")]
        //public IActionResult PatchBolg(int id, [FromBody] BlogDataModel blog)
        //{
        //    BlogResponseModel model = new BlogResponseModel();

        //    AppDbContext db = new AppDbContext();
        //    BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
        //    if (item is null)
        //    {
        //        model.IsSuccess = false;
        //        model.Message = "No data found.";
        //        return NotFound(model);
        //    }
        //    item.Blog_Title = blog.Blog_Title;
        //    item.Blog_Author = blog.Blog_Author;
        //    item.Blog_Content = blog.Blog_Content;

        //    var result = db.SaveChanges();
        //    string message = result > 0 ? "Updating Successful." : "Updating Failed";
        //    model = new BlogResponseModel()
        //    {
        //        IsSuccess = result > 0,
        //        Message = message,
        //    };
        //    return Ok(model);

        //}
        [HttpDelete("{id}")]
        public IActionResult DeleteBolgS(int id)
        {
            string query = $@"DELETE FROM[dbo].[Tbl_Blog] WHERE[Blog_ID] = @Blog_ID";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);

            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Delete Successful." : "Delete Failed.";

            connection.Close();
            BlogResponseModel model = new BlogResponseModel();

            if (result == 0)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }
            
            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Ok(model);
        }
    }
}
