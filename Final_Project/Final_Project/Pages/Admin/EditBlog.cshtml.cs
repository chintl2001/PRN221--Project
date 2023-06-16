using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages.Admin
{
    public class EditBlogModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public EditBlogModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        [BindProperty]
        public Blog blog { get; set; }
        [BindProperty]
        public int id { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            blog = await COFFEEContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Blog? existingBlog = await COFFEEContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            existingBlog.Title = blog.Title;
            existingBlog.Image = blog.Image;
            existingBlog.Content = blog.Content;
            existingBlog.ShortContent = blog.ShortContent;
            existingBlog.Author = blog.Author;
            existingBlog.PublishDate = blog.PublishDate;

            COFFEEContext.Update(existingBlog);

            try
            {
                await COFFEEContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return RedirectToPage("/Admin/BlogList");
        }

    }
}
