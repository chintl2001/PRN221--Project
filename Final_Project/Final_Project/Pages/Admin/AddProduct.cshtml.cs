using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string category { get; set; }
        [BindProperty]
        public string price { get; set; }
        [BindProperty]
        public string description { get; set; }
        [BindProperty]
        public string image { get; set; }

        public AddProductModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {
            Categories = COFFEEContext.Categories.ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product
                {
                    Name = name,
                    CategoryId =  Int32.Parse(category),
                    Price = float.Parse(price),
                    Description = description,
                    Image = image
                };

                COFFEEContext.Products.Add(newProduct);
                await COFFEEContext.SaveChangesAsync();

                return RedirectToPage("/Admin/ProductList");
            }

            return Page();
        }
    }
}
