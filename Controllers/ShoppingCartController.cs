using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NToastNotify;

namespace bookverse.Controllers
{
	public class ShoppingCartController : Controller
	{
		private IUnitOfWork unitOfWork;
		private IToastNotification toastNotification;
		
		
		public ShoppingCartController(IUnitOfWork _unitOfWork,IToastNotification _toastNotification)
		{
			unitOfWork = _unitOfWork;
			toastNotification= _toastNotification;
		}
		public IActionResult Index()
		{
			List<ShoppingCart> c = ViewBag.products;
			return View(c);
		}
		public IActionResult Checkout(double ukupno)
		{
			ViewBag.ukupno = ukupno;
			List<ShoppingCart> c = ViewBag.products;
			return View(c);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddProduct(int id)
		{       
			List<ShoppingCart> cart = new() { };
			foreach (var item in cart)
			{
				if(item.Prod.Id == id) { item.Count++; }
			}
			 cart.Add(new() { Prod = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == id),Count=1 }); 

			
			ViewBag.products = cart;
			toastNotification.AddSuccessToastMessage("Uspesno ste dodali proizvod u korpu");

			return RedirectToAction("Index","Shop");
		}
	}
}
