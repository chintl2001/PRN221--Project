using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Fullname { get; set; }

        private COFFEEContext COFFEEContext;
        public User user { get; set; }
        public RegisterModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Username = Username,
                    Password = Password,
                    Fullname = Fullname
                };

                COFFEEContext.Users.Add(newUser);
                COFFEEContext.SaveChanges();

                return RedirectToPage("/Index");
            }

            return Page();
        }

    }
}
