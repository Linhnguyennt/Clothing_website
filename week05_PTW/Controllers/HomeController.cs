using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;
using week05_PTW.Data;
using week05_PTW.Models;

namespace week05_PTW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebDbContext _context;
      

        public HomeController(ILogger<HomeController> logger, WebDbContext context)
        {
            _logger = logger;
            _context = context;
           
          
        }
        

        public IActionResult Index()
        {

            List<Product> products = new();
            List<Product> listProduct1 = this._context.Products.ToList();
            var p = new Product();
            if (p.Tag== "New")
            {
                products.Add((Product)p);
                ViewBag.groupAll = products;
            }
            else
            {
                return View(listProduct1);
            }
               
            return View(products);
        }
        public IActionResult New()
        {
            //List<Product> products = new List<Product>();
            //var test1 = ctx.Products.GroupBy(x => x.Price <= 400);
            //products.Add((Product)test1);
            //return View(test1);
            
            List<Product> products = new();
            List<Product> listProduct1 = this._context.Products.ToList();
            foreach(var product in listProduct1)
            {
                //var p = new Product();
                //var ketqua = from Product in products
                //             where p.Price == 400
                //             select products;
                //products.Add((Product)ketqua);


                if (product.PriceSale> 0)
                {
                    products.Add(product);
                    ViewBag.groupAll = products;
                }
                else
                {
                    return View(listProduct1);
                }
            }
           
            
            return View();
        }

        //List<Product> products = new List<Product>();
        //var test1 = ctx.Products.GroupBy(x => x.Price <= 400);
        //products.Add((Product) test1);

        public IActionResult BackHome()
        {
           
            return View();
        }

        public IActionResult DetailCategory()
		{
			List<Categories> listProduct = this._context.Categories.ToList();
			ViewBag.groupAll = listProduct;
			return View(listProduct);
		}



		public IActionResult Privacy()
        {
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}