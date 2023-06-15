using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages.Admin
{
    public class UpdateProductModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public UpdateProductModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        [BindProperty]
        public Product product { get; set; }
        [BindProperty]
        public int id { get; set; }
        public List<Category> Categories { get; set; }
    
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Categories = COFFEEContext.Categories.ToList();
            product = await COFFEEContext.Products.FirstOrDefaultAsync(b => b.Id == id);

            if (product == null)
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
           
            Product? existingProduct = await COFFEEContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            existingProduct.Name = product.Name;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Image = product.Image;

            COFFEEContext.Update(existingProduct);

            try
            {
                await COFFEEContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return RedirectToPage("/Admin/ProductList");
        }



    }
}
