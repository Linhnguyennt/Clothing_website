using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using week05_PTW.Data;

using Microsoft.AspNetCore.Http;


namespace week05_PTW.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebDbContext _context;

        public ProductController(WebDbContext context)
        {
            _context = context;

        }
        public IActionResult List(int categoryID)
		{
            //var pro1 = _context.Products.Where(p => p.CategoryId == proId).FirstOrDefault();
            List<Product> proall = this._context.Products.Where(p => p.CategoryId == categoryID).ToList();
           
            return View(proall);
        }


        public IActionResult ListSales()
        {
           
            List<Product> prosales = this._context.Products.Where(p => p.PriceSale != 0).ToList();
            ViewBag.prosale = prosales;
            return View(prosales);
        }
        public IActionResult Slidershow()
        {

            List<Product> prosales = this._context.Products.Where(p => p.PriceSale != 0).ToList();
            ViewBag.prosale = prosales;
            return View(prosales);
        }
        public IActionResult ListNew()
        {

            List<Product> pronew = this._context.Products.Where(p => p.Tag == "New").ToList();
            ViewBag.pronew = pronew;
            return View(pronew);
        }
        public IActionResult ListShoes()
        {

            List<Product> proshoes = this._context.Products.Where(p => p.CategoryId==1).ToList();
            ViewBag.groupAll = proshoes;
            return View(proshoes);
        }

        public PartialViewResult Renderproduct()
        {
            return PartialView(ListShoes());
        }





        // Lấy cart từ Session (danh sách CartItem)
        //public List<CartItem> GetCartItems
        // {
        //     get{
        //         var data= HttpContext.Session.Get<List<CartItem>>(GetCartItems);
        //         if (data ==null){
        //             data =new List<CartItem>();
        //         }
        //         return data;
        //     }
        //     set{
        //         HttpContext.Session.Set("Cart", value);

        //     }

        // }


        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {
            //var cart = HttpContext.Session.Get<List<CartItem>>(CARTKEY);
            //return cart ?? new List<CartItem>();
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
        //[Route("addcart/{productid:int}", Name = "addcart")]
        //public IActionResult AddToCart([FromRoute] int productid)
        //{

        //    var product = _context.Products
        //        .FirstOrDefault(p => p.Id == productid);
        //    if (product == null)
        //        return NotFound("Không có sản phẩm");

        //    // Xử lý đưa vào Cart ...
        //    var cart = GetCartItems();
        //    var cartitem = cart.Find(p => p.product.Id == productid);
        //    if (cartitem != null)
        //    {
        //        // Đã tồn tại, tăng thêm 1
        //        cartitem.quantity++;
        //    }
        //    else
        //    {
        //        //  Thêm mới
        //        cart.Add(new CartItem() { quantity = 1, product = product });
        //    }

        //    // Lưu cart vào Session
        //    SaveCartSession(cart);
        //    // Chuyển đến trang hiện thị Cart
        //    return RedirectToAction(nameof(Cart));
        //}


		

		//// Hiện thị giỏ hàng
		//[Route("/cart", Name = "cart")]
  //      public IActionResult Cart()
  //      {
  //          return View(GetCartItems());
  //      }


		

		///// Cập nhật
		//[Route("/updatecart", Name = "updatecart")]
		//[HttpPost]
		//public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
		//{
		//	// Cập nhật Cart thay đổi số lượng quantity ...
		//	var cart = GetCartItems();
		//	var cartitem = cart.Find(p => p.product.Id == productid);
		//	if (cartitem != null)
		//	{
		//		// Đã tồn tại, tăng thêm 1
		//		cartitem.quantity = quantity;
		//	}
		//	SaveCartSession(cart);
		//	// Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
		//	return RedirectToAction(nameof(Cart));
		//}

		///// xóa item trong cart
		//[Route("/removecart/{productid:int}", Name = "removecart")]
		//public IActionResult RemoveCart([FromRoute] int productid)
		//{
		//	var cart = GetCartItems();
		//	var cartitem = cart.Find(p => p.product.Id == productid);
		//	if (cartitem != null)
		//	{
		//		// Đã tồn tại, tăng thêm 1
		//		cart.Remove(cartitem);
		//	}

		//	SaveCartSession(cart);
		//	return RedirectToAction(nameof(Cart));
		//}


		//[Route("/checkout")]
		//public IActionResult CheckOut()
		//{
		//	// Xử lý khi đặt hàng
		//	return View();
		//}

	



	// GET: ProductController/Details/5
	public IActionResult Details(int proId)
        {
           
          //List<Product> pro1 = _context.Products.Where(p => p.Id == proId).ToList();
			Product pro1 =this._context.Products.Where(p => p.Id == proId).FirstOrDefault();
            return View(pro1);
        }



        public IActionResult Category(int catID)
        {
            Product? pro;
            pro = _context.Products.Find(catID);
            var pro1 = _context.Products.Where(p => p.Id == catID).FirstOrDefault();
           
            return View(pro);
        }
		

		// GET: ProductController/Create
		public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Checkout()
        {
           
            return View("Checkout");
        }
		//public ActionResult saveOrder()
		//{
		//	List<CartItem> cart = (List<CartItem>)Session["cart"];
		//	Order orderProduct = new Order();
  //          orderProduct.CreatedAt = DateTime.Now;
  //          orderProduct.Status = fc["trangthai"];
  //          orderProduct.UserId = "New orderProduct";
  //          de.Orders.Add(orderProduct);
  //          decimal.SaveChanges();


  //          foreach(CartItem item in cart)
  //          {
  //              OrderProduct orderProduct1 = new OrderProduct();
  //              orderProduct1.ProductId = orderProduct1.ProductId;
  //              orderProduct1.Quantity = orderProduct1.Quantity;
  //              orderProduct1.Price = orderProduct1.Product.Price;
  //              de.orderProduct1.add(orderProduct1);
  //              de.saveChanges();


		//	}
		//	return View("Thank you");
		//}
	}
}
