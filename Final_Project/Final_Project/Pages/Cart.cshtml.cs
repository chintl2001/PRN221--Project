using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project.Pages
{
    public class CartModel : PageModel
    {
        private COFFEEContext COFFEEContext;

        public CartModel(COFFEEContext _COFFEEContext)
        {
            COFFEEContext = _COFFEEContext;
        }
        // Định nghĩa model cho trang giỏ hàng
        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng từ Session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();
        }

        public IActionResult OnPostConfirmOrder(string note)
        {
            // Get the cart items from the session
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();

            // Check if the cart is empty
            if (CartItems.Count == 0)
            {
                TempData["ErrorMessage"] = "Your cart is empty. Please add some products before confirming the order.";
                return RedirectToPage("/Cart");
            }

            if (ModelState.IsValid)
            {
                // Create a new order record
                Order order = new Order
                {
                    UserId = 1,
                    OrderDate = DateTime.Now,
                    Note = note
                };
                order.Status = 0;

                COFFEEContext.Orders.Add(order);
                COFFEEContext.SaveChanges();

                // Create order details for each item in the cart
                foreach (var cartItem in CartItems)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price
                    };

                    COFFEEContext.OrderDetails.Add(orderDetail);
                }

                COFFEEContext.SaveChanges();

                // Clear the cart after the order is confirmed
                HttpContext.Session.Remove("CartItems");

                // Redirect to a thank you page or show a confirmation message
                return RedirectToPage("/Index");
            }

            // If there are any validation errors, return to the cart page to show them
            return Page();
        }

    }
}

