using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace Final_Project.Pages
{
    public class IndexModel : PageModel
    {
        private  COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public List<Blog> Blogs { get; set; }
        public string Username;


        public IndexModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {
            ViewData["Username"] = Username = HttpContext.Session.GetString("CurrentUser");
            if(Username == null)
            {
                ViewData["Username"] = "Login";
            }
            Categories = COFFEEContext.Categories
            .OrderByDescending(c => c.Products.Count)
            .Take(4)
            .ToList();
            var topProductIds = COFFEEContext.OrderDetails
        .GroupBy(od => od.ProductId)
        .OrderByDescending(g => g.Count())
        .Take(4)
        .Select(g => g.Key)
        .ToList();

    // Get the actual product objects based on the IDs
    Products = COFFEEContext.Products
        .Where(p => topProductIds.Contains(p.Id)).Take(4)
        .ToList();
            Blogs = COFFEEContext.Blogs.OrderBy(p => Guid.NewGuid()).Take(2).ToList();

        }

        public ActionResult Chat()
        {
            return Page();
        }
    }
}