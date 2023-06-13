using Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private COFFEEContext COFFEEContext;
        public User user { get; set; }
        public LoginModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public IActionResult OnPost()
        {
            user = COFFEEContext.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            if (user != null)
            {
                if (user.Username == "Admin" && user.Password == "Admin")
                {
                    HttpContext.Session.SetString("CurrentUser", user.Fullname);
                    return RedirectToPage("/Admin/ProductList");
                }
                else
                {
                    HttpContext.Session.SetString("CurrentUser", user.Fullname);
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Wrong username or password!";
                return Page();
            }
        }
    }
}
