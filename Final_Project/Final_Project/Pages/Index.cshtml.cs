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
            Categories = COFFEEContext.Categories.ToList();
            Products = COFFEEContext.Products.OrderBy(p => Guid.NewGuid()).Take(8).ToList();
            Blogs = COFFEEContext.Blogs.OrderBy(p => Guid.NewGuid()).Take(2).ToList();

        }
    }
}