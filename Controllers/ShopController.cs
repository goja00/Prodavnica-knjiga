using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;

namespace bookverse.Controllers
{
	public class ShopController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		
		public ShopController(IUnitOfWork _unitOfWork)
		{
			unitOfWork = _unitOfWork;
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

	}
}
