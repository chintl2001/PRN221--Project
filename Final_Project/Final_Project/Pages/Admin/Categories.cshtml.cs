using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class CategoriesModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Category> Categories { get; set; }
        public CategoriesModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {
            Categories = COFFEEContext.Categories.ToList();
        }
    }
}
