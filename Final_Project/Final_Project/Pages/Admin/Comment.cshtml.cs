using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages.Admin
{
    public class CommentModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Comment> Comments { get; set; }

        public CommentModel(COFFEEContext _COFFEEContext)
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
            Comments = COFFEEContext.Comments.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            IQueryable<Comment> query = COFFEEContext.Comments;


            Comments = await query.ToListAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostToggleStatusAsync(int id, int status)
        {
            var comment = await COFFEEContext.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            // Validate that the status value is either 0 or 1
            if (status != 0 && status != 1)
            {
                return BadRequest("Invalid status value.");
            }

            comment.Status = status;
            await COFFEEContext.SaveChangesAsync();

            return RedirectToPage("/Admin/Comment");
        }
    }
}