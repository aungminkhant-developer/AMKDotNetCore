﻿using AMKDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMKDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public async Task<IActionResult> BlogIndex(int pageNo = 1, int pageSize = 10)
        {

            List<BlogDataModel> lst = await _context.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int pageRowCount = await _context.Blogs.CountAsync();
            int pageCount = pageRowCount / pageSize;
            if (pageCount % pageSize > 0)
                pageCount++;

            BlogListResponseModel model = new BlogListResponseModel
            {
                BlogList = lst,
                pageCount = pageCount,
                pageNo = pageNo,
                pageSize = pageSize,
                pageRowCount = pageRowCount
            };


            return View("BlogIndex", model);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {

            
            
            return View("BlogCreate");
        }

       
        public async Task <IActionResult> Generate()
        {
            for (int i=1;i<= 1000; i++)
            {
                await _context.AddAsync(new BlogDataModel
                {
                    Blog_Title = i.ToString(),
                    Blog_Author = i.ToString(), 
                    Blog_Content = i.ToString(),    
                }
                    );
                var result = await _context.SaveChangesAsync(); 

            }


            return Redirect("/Blog");
        }




        [HttpPost]
        [ActionName("Save")]
        public async Task <IActionResult> BlogSave(BlogDataModel blog)
        {
            await _context.AddAsync(blog);
            var result  = await _context.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Redirect("/Blog");
        }

        [ActionName("Edit")]
        public async Task <IActionResult> BlogEdit(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id==id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {

                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }

            return View("BlogEdit",item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id ,BlogDataModel blog)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);

            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {

                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }
            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author= blog.Blog_Author; 
            item.Blog_Content= blog.Blog_Content;   


            var result = await _context.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Redirect("/Blog");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }

            var item = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {

                TempData["IsSuccess"] = false;
                TempData["Message"] = "No Data Found.";
                return Redirect("/Blog");
            }

            _context.Blogs.Remove(item);    
            var result = await _context.SaveChangesAsync();

            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            return Redirect("/Blog");
        }
    }
}