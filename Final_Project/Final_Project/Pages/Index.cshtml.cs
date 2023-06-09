using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class IndexModel : PageModel
    {
        private  COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public List<Blog> Blogs { get; set; }

        public IndexModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {
            Categories = COFFEEContext.Categories.ToList();
            Products = COFFEEContext.Products.OrderBy(p => Guid.NewGuid()).Take(8).ToList();
            Blogs = COFFEEContext.Blogs.OrderBy(p => Guid.NewGuid()).Take(2).ToList();

        }
    }
}