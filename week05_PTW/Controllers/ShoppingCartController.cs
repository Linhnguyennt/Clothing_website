using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using week05_PTW.Data;
using week05_PTW.Models;

namespace week05_PTW.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly WebDbContext _context;

        public ShoppingCartController(WebDbContext context)
        {
            _context = context;

        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }


        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }


        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Products
                .FirstOrDefault(p => p.Id == productid);
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }


        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return RedirectToAction(nameof(Cart));
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        //[Route("/checkout")]
        //public IActionResult CheckOut()
        //{

        //	return View(GetCheckoutViewModel());
        //}

        //[HttpPost]
        //public IActionResult CheckOut(CheckoutViewModel request){
        //	var model = GetCheckoutViewModel();
        //	var orderDetail = new List<OrderProduct>();
        //	foreach(var item in model.CartItems) {
        //		orderDetail.Add(new OrderProduct()
        //		{
        //			ProductId = (int)item.productid,
        //			Quantity = item.quantity
        //		});
        //	}
        //	var checkoutRequest = new CheckoutRequest()
        //	{
        //		Address = request.CheckoutModel.Address,
        //		Name = request.CheckoutModel.Name,
        //		Email = request.CheckoutModel.Email,
        //		PhoneNumber = request.CheckoutModel.PhoneNumber,
        //		CreatedAt = request.CheckoutModel.CreatedAt,

        //	};
        //	TempData["SucessMsg"] = "Order puschased sucessful";

        //	// Xử lý khi đặt hàng
        //	return View(model);
        //}



        //public ActionResult Index()
        //{
        //	Session[CommonConstants.SessionCart] =new List<ShoppingCartViewModel>();
        //	return View();
        //}
        //[HttpPost]
        //public JsonResult Add(int productid)
        //{
        //	var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
        //	if (cart.Any(x => x.ProductId == productid))
        //	{
        //		foreach (var item in cart)
        //		{
        //			if (item.ProductId == productid)
        //			{
        //				item.Quanlity += 1;
        //			}

        //		else {
        //			ShoppingCartViewModel newItem = new ShoppingCartViewModel();
        //			newItem.ProductId = productid;
        //			var product1 = _productService.GetById(productid);
        //			newItem.Product = Mapper.Map<Product, Product>(product1);
        //				newItem.Quanlity = 1;
        //		}
        //			Session[CommonConstants.SessionCart] = cart;
        //			return Json(new
        //			{
        //				status = true
        //			});
        //	}
        //	}
        //}
        //private CheckoutViewModel GetCheckoutViewModel()
        //{
        //    var session = HttpContext.Session;
        //    string jsoncart = session.GetString(CARTKEY);
        //    List<CartItemViewModel> CurrentCart = new List<CartItemViewModel>();
        //    if (jsoncart != null)
        //    {
        //        CurrentCart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(jsoncart);
        //    }

        //    var checkout = new CheckoutViewModel()
        //    {
        //        CartItems = CurrentCart,
        //        CheckoutModel = new CheckoutRequest()
        //    };
        //    return checkout;
        //}


        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutRequest model)
        {
            var returnUrl = Url.Content("~/");
            //List<CartItem> item= new List<CartItem>();
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                Order ddh = new Order();
                ddh.Name = model.Name;
                ddh.Email = model.Email;
                ddh.Address = model.Address;
                ddh.PhoneNumber = model.PhoneNumber;
                ddh.CreatedAt = DateTime.Now;
                _context.Orders.Add(ddh);
                //_context.Orders.Add(ddh);
                await _context.SaveChangesAsync();

                var cartItem = GetCartItems();

                foreach (var cartitem in cartItem)
                {
                    var product = new OrderProduct()
                    {
                        ProductId = cartitem.product.Id,
                        OrderId = ddh.Id,
                        Quantity = cartitem.quantity,
                        Price = cartitem.quantity * cartitem.product.Price
                    };
                    _context.OrderProducts.Add(product);
                }
                await _context.SaveChangesAsync();

                ClearCart();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
