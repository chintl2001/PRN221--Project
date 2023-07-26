using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Pages.Admin
{
    public class DeleteCategoryModel : PageModel
    {
        private COFFEEContext COFFEEContext;

        public DeleteCategoryModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }

        public Category Category { get; set; }

        [BindProperty]
        public int id { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = COFFEEContext.Categories.FirstOrDefault(p => p.Id == id);
            if (Category == null)
            {
                return NotFound();
            }

            // Check if there are associated products with this category
            var productsWithCategory = COFFEEContext.Products.Any(p => p.CategoryId ==  id);
            if (productsWithCategory)
            {
                TempData["ErrorMessage"] = "Category has associated products and cannot be deleted.";
                return RedirectToPage("/Admin/Categories");
            }

            // If no associated products, proceed with deletion
            COFFEEContext.Categories.Remove(Category);
            await COFFEEContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Categories");
        }
    }
}
