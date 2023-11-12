namespace AMKDotNetCore.MvcApp.Models
{
    public class BlogListResponseModel
    {
        public List<BlogDataModel> BlogList { get; set; }
        public int pageNo { get; set; } 
        public int pageSize { get; set; }   
        public int pageCount { get; set; } 
        public int pageRowCount { get; set; }
        
    }

   
}
