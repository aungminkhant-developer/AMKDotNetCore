using AMKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AMKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBolgS()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.ToList();
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
            BlogResponseModel model = new BlogResponseModel();

            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }
            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            return Ok(model);
        }
        [HttpPost]
        public IActionResult CreateBlog([FromBody]BlogDataModel blog)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed";
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
                Data = blog
                
            };
            return Ok(model);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,[FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
             model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBolg(int id, [FromBody] BlogDataModel blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed";
            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBolgS(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            AppDbContext db = new AppDbContext();
            BlogDataModel item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }
            db.Blogs.Remove(item);
            var result = db.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed";
            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,

            };
           
            return Ok(model);
        }
    }
}
