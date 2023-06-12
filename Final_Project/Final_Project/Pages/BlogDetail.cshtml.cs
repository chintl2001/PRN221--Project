using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages
{
    public class BlogDetail : PageModel
    {
        [BindProperty]
        public string Title { get; set; }

        private COFFEEContext COFFEEContext;
        public Blog Blog { get; set; }
        public List<Blog> recentBlogs { get; set; }

        public BlogDetail(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public string Username;
            public async Task<IActionResult> OnGetAsync(int id)
            {
            ViewData["Username"] = Username = HttpContext.Session.GetString("CurrentUser");
            if (Username == null)
            {
                ViewData["Username"] = "Login";
            }
            recentBlogs = COFFEEContext.Blogs.OrderByDescending(b => b.PublishDate).Take(5).ToList();
            Blog = await COFFEEContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (Blog == null)
            {
                return NotFound();
            }

            return Page();
        }


    }
}