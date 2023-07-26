using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class CartModel : PageModel
    {
        // Định nghĩa model cho trang giỏ hàng
        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng từ Session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();
        }
    }
}

/*public class CartItem
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public int? Quantity { get; set; }
    public float? Price { get; set; }
}*/