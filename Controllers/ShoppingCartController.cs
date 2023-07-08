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
		[Authorize]
		public IActionResult Index()
		{
			var claim = (ClaimsIdentity)User.Identity;
			var t = claim.FindFirst(ClaimTypes.NameIdentifier);

			scVM c = new scVM()
			{
				ListCart = unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == t.Value,
				includeProperties: "Prod"),
				OrderHeader = new Models.OrderHeader()
			};

			foreach(var item in c.ListCart)
			{
				c.OrderHeader.OrderTotal += item.Prod.Price * item.Count;
			}
			return View(c);
		}
		[Authorize]
		public IActionResult Checkout()
		{	
			var claim = (ClaimsIdentity)User.Identity;
			var t = claim.FindFirst(ClaimTypes.NameIdentifier);

			sc= new scVM()
			{
				ListCart = unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == t.Value,
				includeProperties: "Prod"),
				OrderHeader = new Models.OrderHeader()
			};

			foreach (var item in sc.ListCart)
			{
				sc.OrderHeader.OrderTotal += item.Prod.Price * item.Count;
			}


			sc.OrderHeader.ApplicationUser = unitOfWork.applicationUserRepository.GetFirstOrDefault(x => x.Id == t.Value);

			sc.OrderHeader.FirstName = sc.OrderHeader.ApplicationUser.FirstName;
			sc.OrderHeader.LastName= sc.OrderHeader.ApplicationUser.LastName;
			sc.OrderHeader.Address = sc.OrderHeader.ApplicationUser.Address;
			sc.OrderHeader.Town = sc.OrderHeader.ApplicationUser.Town;
			sc.OrderHeader.ZipCode = (int)sc.OrderHeader.ApplicationUser.ZipCode;
			sc.OrderHeader.PhoneNumber = sc.OrderHeader.ApplicationUser.PhoneNumber;

			return View(sc);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult OrderPOST(scVM cartOrder)
		{
            var claim = (ClaimsIdentity)User.Identity;
            var t = claim.FindFirst(ClaimTypes.NameIdentifier);
			
			cartOrder.ListCart = unitOfWork.shoppingCartRepository.GetAll(x => x.ApplicationUserId == t.Value, includeProperties: "Prod");
			
			cartOrder.OrderHeader.OrderStatus = OrderStatus.Pending;
			cartOrder.OrderHeader.PaymentStatus = OrderStatus.Pending;
			cartOrder.OrderHeader.OrderDate=DateTime.Now;

			cartOrder.OrderHeader.ApplicationUser = unitOfWork.applicationUserRepository.GetFirstOrDefault(x => x.Id == t.Value);

			cartOrder.OrderHeader.FirstName = cartOrder.OrderHeader.ApplicationUser.FirstName;
			cartOrder.OrderHeader.LastName = cartOrder.OrderHeader.ApplicationUser.LastName;
			cartOrder.OrderHeader.Address = cartOrder.OrderHeader.ApplicationUser.Address;
			cartOrder.OrderHeader.Town = cartOrder.OrderHeader.ApplicationUser.Town;
			cartOrder.OrderHeader.ZipCode = (int)cartOrder.OrderHeader.ApplicationUser.ZipCode;
			cartOrder.OrderHeader.PhoneNumber = cartOrder.OrderHeader.ApplicationUser.PhoneNumber;


			foreach (var item in cartOrder.ListCart)
            {
                cartOrder.OrderHeader.OrderTotal += item.Prod.Price * item.Count;
            }
			
			unitOfWork.orderHeaderRepository.Add(cartOrder.OrderHeader);
			unitOfWork.save();

            foreach (var item in cartOrder.ListCart)
			{
				OrderDetails details = new()
				{
					ProductId = item.ProductId,
					OrderId = cartOrder.OrderHeader.Id,
					Price = item.Prod.Price,
					Count = item.Count
				};
				unitOfWork.orderDetailsRepository.Add(details);
				
				unitOfWork.save();
			}

			unitOfWork.shoppingCartRepository.RemoveRange(cartOrder.ListCart);
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

		
        [Authorize]
        public IActionResult AddProduct(int id)
        {
			Product p=unitOfWork.productRepository.GetFirstOrDefault(x=>x.Id==id);
			p.Count = 1;
			
            var claim = (ClaimsIdentity)User.Identity;
            var t = claim.FindFirst(ClaimTypes.NameIdentifier);

            Shopping_Cart cart = unitOfWork.shoppingCartRepository.GetFirstOrDefault(u => u.ApplicationUserId == t.Value &&
            u.ProductId == p.Id);
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

            return RedirectToAction("Index", "Shop");
        }
        [Authorize]
        public IActionResult Remove(int id)
		{
			Shopping_Cart sc=unitOfWork.shoppingCartRepository.GetFirstOrDefault(x=>x.Id==id);
			unitOfWork.shoppingCartRepository.Delete(sc);
			unitOfWork.save();
			return RedirectToAction("Index");
		}

	}
}
