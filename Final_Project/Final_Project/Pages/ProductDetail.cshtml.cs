using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages.Shared
{
    public class ProductDetailModel : PageModel
    {
        private readonly COFFEEContext _coffeeContext;

        public Product Product { get; set; }
        public List<Blog> Blogs { get; set; }
        public ProductDetailModel(COFFEEContext coffeeContext)
        {
            _coffeeContext = coffeeContext;
        }
        public string Username;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _coffeeContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            Blogs = _coffeeContext.Blogs.OrderBy(p => Guid.NewGuid()).Take(2).ToList();
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
