using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages.Admin
{
    public class ProductListModel : PageModel
    {
        private COFFEEContext COFFEEContext;
        public List<Product> products { get; set; }
        public string Username;

        public ProductListModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public void OnGet()
        {
        }
    }
}
