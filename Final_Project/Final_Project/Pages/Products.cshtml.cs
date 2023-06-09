using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class ProductModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }


        public ProductModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }

        public void OnGet()
        {
            Categories = COFFEEContext.Categories.ToList();
            Products = COFFEEContext.Products.ToList();
        }
    }
}