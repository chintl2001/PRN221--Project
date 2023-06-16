using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class DeleteBlogModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public DeleteBlogModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public Blog blog { get; set; }
        [BindProperty]
        public int id { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            blog = COFFEEContext.Blogs.FirstOrDefault(b => b.Id == id);
            COFFEEContext.Blogs.Remove(blog);
            await COFFEEContext.SaveChangesAsync();
            return RedirectToPage("/Admin/BlogList");
        }
    }
}