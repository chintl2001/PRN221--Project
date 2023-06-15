using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class AddBlogModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        [BindProperty]
        public string title { get; set; }
        [BindProperty]
        public string image { get; set; }
        [BindProperty]
        public string content { get; set; }
        [BindProperty]
        public string shortContent { get; set; }
        [BindProperty]
        public string auther { get; set; }
        [BindProperty]
        public DateTimeOffset publishDate { get; set; }

        public AddBlogModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Blog addBlog = new Blog
                {
                    Title = title,
                    Image = image,
                    Content = content,
                    ShortContent = shortContent,
                    Author = auther,
                    PublishDate = TimeZoneInfo.ConvertTimeToUtc(publishDate.LocalDateTime, TimeZoneInfo.Local)
                };

                COFFEEContext.Blogs.Add(addBlog);
                await COFFEEContext.SaveChangesAsync();

                return RedirectToPage("/Admin/BlogList");
            }

            return Page();
        }
    }
}
