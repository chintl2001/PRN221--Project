using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages
{
    public class BlogsModel : PageModel
    {

        [BindProperty]
        public string Title { get; set; }

        private COFFEEContext COFFEEContext;
        public List<Blog> blogs { get; set; }
        public List<Blog> recentBlogs { get; set; }
        public string Username;

        public BlogsModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }

        public void OnGet()
        {
            blogs = COFFEEContext.Blogs.ToList();
            recentBlogs = COFFEEContext.Blogs.OrderByDescending(b => b.PublishDate).Take(5).ToList();
            ViewData["Username"] = Username = HttpContext.Session.GetString("CurrentUser");
            if (Username == null)
            {
                ViewData["Username"] = "Login";
            }

        }

        public IActionResult OnPost()
        {
            blogs = COFFEEContext.Blogs.Where(b => b.Title.Contains(Title)).ToList();
            if(Title == null)
            {
                blogs = COFFEEContext.Blogs.ToList();
            }
            return Page();
        }

    }
}