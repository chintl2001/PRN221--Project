using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class BlogsModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        private List<Blog> Blogs;


        public BlogsModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }

        public void OnGet()
        {
            Blogs = COFFEEContext.Blogs.ToList();
        }
    }
}