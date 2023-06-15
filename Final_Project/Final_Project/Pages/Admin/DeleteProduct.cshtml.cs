using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class DeleteProductModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public DeleteProductModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public Product product { get; set; }
        [BindProperty]
        public int id { get; set; }
        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            product = COFFEEContext.Products.FirstOrDefault(p => p.Id == id);
            COFFEEContext.Products.Remove(product);
            await COFFEEContext.SaveChangesAsync();
            return RedirectToPage("/Admin/ProductList");
        }
    }
}
