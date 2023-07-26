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
        public List<Comment> ApprovedComments { get; set; }
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


            ApprovedComments = await COFFEEContext.Comments
                .Where(c => c.Status == 1 && c.BlogId == id)
                .ToListAsync();

            if (Blog == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAddCommentAsync(int id, string commentContent)
        {
            // Validate input (e.g., check if commentContent is not empty)
            if (string.IsNullOrEmpty(commentContent))
            {
                return BadRequest("Comment content cannot be empty.");
            }

            // Create a new Comment object
            var newComment = new Comment
            {
                BlogId = id,
                UserId = null, // You can set the UserId if you have user authentication
                Comment1 = commentContent,
                Date = DateTime.Now,
                Status = 0 // Set the status to 0 (pending)
            };

            // Add the new comment to the database
            COFFEEContext.Comments.Add(newComment);
            await COFFEEContext.SaveChangesAsync();

            // Redirect back to the BlogDetail page to refresh the comments section
            return RedirectToPage("/BlogDetail", new { id });
        }
    }
}