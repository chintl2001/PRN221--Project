using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages.Admin
{
    public class BlogListModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Blog> Blogs { get; set; }

        [BindProperty(Name = "searchText")]
        public string SearchText { get; set; }
        public BlogListModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public string Username;

        public void OnGet()
        {
            ViewData["Username"] = Username = HttpContext.Session.GetString("CurrentUser");
            if (Username == null)
            {
                ViewData["Username"] = "Login";
            }
            Blogs = COFFEEContext.Blogs.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            IQueryable<Blog> query = COFFEEContext.Blogs;


            Blogs = await query.ToListAsync();

            return Page();
        }
    }
}
