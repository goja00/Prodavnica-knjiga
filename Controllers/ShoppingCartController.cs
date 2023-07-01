using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NToastNotify;
using System.Security.Claims;

namespace bookverse.Controllers
{
	public class ShoppingCartController : Controller
	{
		private IUnitOfWork unitOfWork;
		private IToastNotification toastNotification;
		public scVM sc { get; set; }	
		

		public ShoppingCartController(IUnitOfWork _unitOfWork,IToastNotification _toastNotification)
		{
			unitOfWork = _unitOfWork;
			toastNotification= _toastNotification;
		}
		public IActionResult Index()
		{
			var claim = (ClaimsIdentity)User.Identity;
			var t = claim.FindFirst(ClaimTypes.NameIdentifier);

			sc = new scVM()
			{
				ListCart = unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == t.Value,
				includeProperties: "Prod"),
				OrderHeader = new Models.OrderHeader()
			};

			foreach(var item in sc.ListCart)
			{
				sc.OrderHeader.OrderTotal += item.Prod.Price * item.Count;
			}
			return View(sc);
		}
		public IActionResult Checkout()
		{	
			var claim = (ClaimsIdentity)User.Identity;
			var t = claim.FindFirst(ClaimTypes.NameIdentifier);

			scVM cart = new scVM()
			{
				ListCart = unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == t.Value,
				includeProperties: "Prod"),
				OrderHeader = new Models.OrderHeader()
			};

			foreach (var item in cart.ListCart)
			{
				cart.OrderHeader.OrderTotal += item.Prod.Price * item.Count;
			}


			cart.OrderHeader.ApplicationUser = unitOfWork.applicationUserRepository.GetFirstOrDefault(x => x.Id == t.Value);

			cart.OrderHeader.FirstName = cart.OrderHeader.ApplicationUser.FirstName;
			cart.OrderHeader.LastName= cart.OrderHeader.ApplicationUser.LastName;
			cart.OrderHeader.Address = cart.OrderHeader.ApplicationUser.Address;
			cart.OrderHeader.Town = cart.OrderHeader.ApplicationUser.Town;
			cart.OrderHeader.ZipCode = (int)cart.OrderHeader.ApplicationUser.ZipCode;
			cart.OrderHeader.PhoneNumber = cart.OrderHeader.ApplicationUser.PhoneNumber;

			return View(cart);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult OrderPOST(scVM order)
		{
			order.OrderHeader.OrderStatus = OrderStatus.Pending;
			order.OrderHeader.PaymentStatus = OrderStatus.Pending;
			order.OrderHeader.OrderDate=DateTime.Now;
			
			foreach(var item in order.ListCart)
			{
				OrderDetails details = new()
				{
					ProductId = item.ProductId,
					OrderId = order.OrderHeader.Id,
					Price = item.Prod.Price,
					Count = item.Count
				};
				unitOfWork.orderDetailsRepository.Add(details);
				unitOfWork.shoppingCartRepository.RemoveRange(order.ListCart);
				unitOfWork.save();
			}

			unitOfWork.orderHeaderRepository.Add(order.OrderHeader);
			unitOfWork.save();

			toastNotification.AddSuccessToastMessage("Porudzbina je uspesno zabelezena!");
			return RedirectToAction("Index","Home");
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult AddProduct(Product p)
		{
			var claim = (ClaimsIdentity)User.Identity;
			var t = claim.FindFirst(ClaimTypes.NameIdentifier);

			Shopping_Cart cart = unitOfWork.shoppingCartRepository.GetFirstOrDefault(u => u.ApplicationUserId == t.Value &&
			u.ProductId ==p.Id);
			if (cart == null)
			{
				Shopping_Cart sc = new Shopping_Cart()
				{
					ApplicationUserId = t.Value,
					Prod = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == p.Id),
					ProductId = p.Id,
					Count = p.Count
                };
                unitOfWork.shoppingCartRepository.Add(sc);
			}
			else
			{
				unitOfWork.shoppingCartRepository.IncrementCount(cart, p.Count);
			}

			unitOfWork.save();

			toastNotification.AddSuccessToastMessage("Uspesno ste dodali proizvod u korpu");

			return RedirectToAction("Index","Shop");
		}

		public IActionResult Remove(int id)
		{
			Shopping_Cart sc=unitOfWork.shoppingCartRepository.GetFirstOrDefault(x=>x.Id==id);
			unitOfWork.shoppingCartRepository.Delete(sc);
			unitOfWork.save();
			return RedirectToAction("Index");
		}

	}
}
