using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using NuGet.Protocol;

namespace bookverse.Controllers
{
	public class ShopController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private IToastNotification toast;


		public ShopController(IUnitOfWork _unitOfWork, IToastNotification _toast)
		{
			unitOfWork = _unitOfWork;
			toast= _toast;
		}
		public IActionResult Index()
		{
			IEnumerable<Product> p =unitOfWork.productRepository.GetAll();
			ViewBag.categoriesList = GetCategories();
			ViewBag.coverTypeList=GetCoverTypes();
			
			return View(p);
		}

		public IActionResult ToHome()
		{
			return RedirectToAction("Index", "Home");
		}

		public IEnumerable<Category> GetCategories()
		{
			return unitOfWork.categoryRepository.GetAll();
		}

		public IEnumerable<CoverType> GetCoverTypes()
		{
			return unitOfWork.coverTypeRepository.GetAll();
		}

		public IActionResult FilterByCategory(string filter)
		{
			List<Product> products = unitOfWork.productRepository.GetAll(includeProperties: "c").Where(x => x.c.Name == filter).ToList();

			ViewBag.categoriesList = GetCategories();
			ViewBag.coverTypeList = GetCoverTypes();
			if (products.Count == 0)
			{
				toast.AddWarningToastMessage("Nema proizvoda sa tom kategorijom");
				return RedirectToAction("Index");
			}
			else {
				return View("Index", products);
			}
        }
		public IActionResult FilterByCoverType(string filter)
		{
			List<Product> products = unitOfWork.productRepository.GetAll(includeProperties: "ct").Where(x => x.ct.Name == filter).ToList();
			
			ViewBag.categoriesList = GetCategories();
			ViewBag.coverTypeList = GetCoverTypes();
			if (products.Count ==0 )
			{
				toast.AddWarningToastMessage("Nema proizvoda sa tim povezom");
				return RedirectToAction("Index");
			}
			else
			{
				return View("Index", products);
			}

			
		}

		public IActionResult FilterByPrice(int price)
		{
			List<Product> products = unitOfWork.productRepository.GetAll().Where(x => x.Price == price).ToList();
			ViewBag.categoriesList = GetCategories();
			ViewBag.coverTypeList = GetCoverTypes();

			if (products.Count==0)
			{
				toast.AddWarningToastMessage("Nema proizvoda sa tom cenom");
				return RedirectToAction("Index");
			}
			else
			{
				return View("Index", products);
			}
			
		}
	}
}
