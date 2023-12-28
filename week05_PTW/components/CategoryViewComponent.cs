using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using week05_PTW.Data;

namespace week05_PTW.components
{
    public class CategoryViewComponent : ViewComponent
    {
        // GET: CategoryViewComponent
        private readonly WebDbContext _context;
        public CategoryViewComponent (WebDbContext context)
        {
            this._context = context;
        }

		// GET: CategoryViewComponent/Details/5
		//     public async Task<IViewComponentResult> InvokeAsync()
		//     {
		//         List<Categories> cateList=await _context.Categories.ToListAsync();
		//ViewBag.groupAll = cateList;
		//return View(cateList);
		//     }

	
		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Categories> cateList = await _context.Categories.ToListAsync();
			ViewBag.groupAll = cateList;
			return View(cateList);
		}

		//// GET: CategoryViewComponent/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		// POST: CategoryViewComponent/Create
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create(IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		// GET: CategoryViewComponent/Edit/5
		//public ActionResult Edit(int id)
		//{
		//    return View();
		//}

		// POST: CategoryViewComponent/Edit/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit(int id, IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		// GET: CategoryViewComponent/Delete/5
		//public ActionResult Delete(int id)
		//{
		//    return View();
		//}

		// POST: CategoryViewComponent/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}
	}
}
