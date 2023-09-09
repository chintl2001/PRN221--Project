using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;
namespace Final_Project.Pages.Shared
{
    public class ProductDetailModel : PageModel
    {
        private readonly COFFEEContext _coffeeContext;
        private List<CartItem> CartItems;

        public Product Product { get; set; }
        public List<Blog> Blogs { get; set; }

        public ProductDetailModel(COFFEEContext coffeeContext)
        {
            _coffeeContext = coffeeContext;
            CartItems = new List<CartItem>();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _coffeeContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            Blogs = _coffeeContext.Blogs.OrderBy(p => Guid.NewGuid()).Take(2).ToList();
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
        public IActionResult OnPostAddToCart(int productId, int quantity)
        {
            Product product = _coffeeContext.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartItems") ?? new List<CartItem>();

            CartItem existingCartItem = CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Add the product to the cart
                CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Price = product.Price
                });
            }

            // Save the updated CartItems back to the session
            HttpContext.Session.SetObjectAsJson("CartItems", CartItems);

            // Redirect to the Cart page
            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRating(int productId, float rate)
        {
            Product = _coffeeContext.Products.FirstOrDefault(p => p.Id == productId);
            if (Product == null)
            {
                return NotFound();
            }

            // Kiểm tra xem giá trị đánh giá hợp lệ (từ 0 đến 5)
            if (rate >= 0 && rate <= 5)
            {
                Product.Rate = rate;
                _coffeeContext.SaveChanges();
            }
            else
            {
                // Xử lý nếu giá trị đánh giá không hợp lệ
                // ...
            }

            // Sau khi cập nhật đánh giá, chuyển hướng trở lại trang chi tiết sản phẩm
            return RedirectToPage("/ProductDetail", new { id = Product.Id });
        }
    }
      
}
