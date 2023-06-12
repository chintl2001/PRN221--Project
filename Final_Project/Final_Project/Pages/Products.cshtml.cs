using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Pages
{
    public class ProductModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        [BindProperty(Name = "searchText")]
        public string SearchText { get; set; }

        [BindProperty(Name = "category")]
        public string Category { get; set; }

        [BindProperty(Name = "price")]
        public string Price { get; set; }

        public ProductModel(COFFEEContext _COFFEEContext)
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
            Categories = COFFEEContext.Categories.ToList();
            Products = COFFEEContext.Products.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Categories = COFFEEContext.Categories.ToList();
            IQueryable<Product> query = COFFEEContext.Products;

            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(p => p.Name.Contains(SearchText) ||
                                         p.Category.Name.Contains(SearchText) ||
                                         p.Price.ToString().Contains(SearchText));
            }

            if (!string.IsNullOrEmpty(Category))
            {
                int categoryId = int.Parse(Category);
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(Price))
            {
                if (Price == "ascending")
                {
                    query = query.OrderBy(p => p.Price);
                }
                else if (Price == "descending")
                {
                    query = query.OrderByDescending(p => p.Price);
                }
            }

            Products = await query.ToListAsync();

            return Page();
        }

    }
}