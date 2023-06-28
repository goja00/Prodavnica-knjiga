using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;

namespace bookverse.Controllers
{
    public class ProductController : Controller
    {
		private IUnitOfWork unitOfWork;
		private IWebHostEnvironment webHostEnvironment;

		public ProductController(IUnitOfWork _unitOfWork,IWebHostEnvironment _webHostEnvironment)
		{
			unitOfWork= _unitOfWork;
			webHostEnvironment= _webHostEnvironment; 
		}
		public IActionResult Index(int? id)
		{
			Product p = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == id);
			return View(p);
		}

		[HttpPost]
        public IActionResult Create(Product p,IFormFile file)
        {
			if (ModelState.IsValid)
			{
				string wwwrootpath = webHostEnvironment.WebRootPath;

				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString();
					var upload = wwwrootpath + "\\images\\";
					var extension = Path.GetExtension(file.FileName);

					using (var stream = new FileStream(upload + fileName + extension, FileMode.Create))
					{
						file.CopyTo(stream);
					}
					p.ImageURL = "\\images\\" + fileName + extension;

				}

				unitOfWork.productRepository.Add(p);
				unitOfWork.save();
				
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");


		}
    }
}
