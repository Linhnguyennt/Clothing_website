using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Linq;
using week05_PTW.Data;

namespace week05_PTW.Controllers
{
    public class CategoryController : Controller
    {
        private readonly WebDbContext _context;

        public CategoryController( WebDbContext context)
        {
            _context = context;
        }

        public IActionResult Category(int catID)
        {
            List<Categories> listCategories = this._context.Categories.Where(p => p.Id == catID).ToList();
            //ViewBag.groupCategory=listCategories;
            
            return View(listCategories);
        }
     
        public IActionResult Index()
        {
            return View();
        }
       
        

    }
}
