using Microsoft.AspNetCore.Mvc;
using week05_PTW.Data;

namespace week05_PTW.Models
{
	[Serializable]
	public partial class ShoppingCartViewModel
	{
		WebDbContext webDb =new WebDbContext();
		//public ActionResult checkout()
		//{
		//	if (Session[CommonConstants.SessionCart]==null)
		//}
		//public int GetCount()
		//{
		//	// Get the count of each item in the cart and sum them up
		//	int? count = (from cartItems in webDb.CartItem
		//				  where cartItems.CartId == ShoppingCartId
		//				  select (int?)cartItems.Count).Sum();
		//	// Return 0 if all entries are null
		//	return count ?? 0;
		//}
	}
}
