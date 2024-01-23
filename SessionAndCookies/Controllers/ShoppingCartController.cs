using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SessionAndCookies.Models;

namespace SessionAndCookies.Controllers
{
    [Route("cart/[controller]")]
    public class ShoppingCartController : Controller
    {
        /// <summary>
        /// Get cart.
        /// If not exist, create new cart and insert data.
        /// Else, insert data to existing cart.
        /// Update session cart
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Item> ShoppingCart(string itemName, int price)
        {
            List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");

            if (cart == null)
            {
                cart = new List<Item>()
                {
                    new Item(itemName, price)
                };

                HttpContext.Session.SetObjectAsJson("cart", cart);

                return cart;
            }

            cart.Add(new Item(itemName, price));
            HttpContext.Session.SetObjectAsJson("cart", cart);

            return cart;

        }

        /// <summary>
        /// Find item in cart by param.
        /// Delete item from cart.
        /// Update session cart.
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        [HttpDelete]
        public string RemoveItem(string itemName)
        {

            if (!string.IsNullOrEmpty(itemName))
                try
                {
                    List<Item> cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                    Item itemToBeRemoved = cart.FirstOrDefault(x => x.Name == itemName);

                    if (itemToBeRemoved != null)
                    {
                        cart.Remove(itemToBeRemoved);
                        HttpContext.Session.SetObjectAsJson("cart", cart);
                        return $"[SUCCESS] Removed {itemName} from cart";
                    }

                    return "Not found";
                }
                catch (ArgumentNullException) { return "[FAIL] No items in cart | cart does not exist"; }

            else
                return "Invalid Name";
        }
    }
}
