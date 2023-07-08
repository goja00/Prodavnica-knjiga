using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace bookverse.Controllers
{
    public class ProductController : Controller
    {
		private IUnitOfWork unitOfWork;
		private IWebHostEnvironment webHostEnvironment;
		private IToastNotification toaster;

		public ProductController(IUnitOfWork _unitOfWork,IWebHostEnvironment _webHostEnvironment,IToastNotification toast)
		{
			unitOfWork= _unitOfWork;
			webHostEnvironment= _webHostEnvironment;
			toaster = toast;
		}
		public IActionResult Index(int? id)
		{
			Product p = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == id);
			return View(p);
		}

		[HttpGet]
        
        public IActionResult GetAll()
        {
            IEnumerable<SelectListItem> temp = unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            IEnumerable<SelectListItem> tempC = unitOfWork.coverTypeRepository.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

            ViewBag.CoverList = tempC;
            ViewBag.CategoryList = temp;

           ViewBag.productList = unitOfWork.productRepository.GetAll(includeProperties:"c,ct").ToList();	
            return View();
        }
		[HttpGet]
        
        public IActionResult GetOne(int id)
		{
            Product p = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == id,includeProperties:"c,ct");
			return Json(p);

        }
        [HttpGet]
        
        public IActionResult GetAllJson()
        {
            var p = unitOfWork.productRepository.GetAll(includeProperties: "c,ct");
            return Json(p);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			
			var prod = unitOfWork.productRepository.GetFirstOrDefault(x => x.Id == id);
			if(prod !=null) 
			{
				unitOfWork.productRepository.Delete(prod);
				unitOfWork.save();
			}
            toaster.AddSuccessToastMessage("Proizvod je uspesno izbrisan!");
            return RedirectToAction("Index","CMS");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product p,IFormFile? file)
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
				toaster.AddSuccessToastMessage("Proizvod je uspesno dodat!");
				return RedirectToAction("Index", "CMS");
            }
			return RedirectToAction("Index", "CMS");


        }

		[HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Product p, IFormFile? file)
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

                unitOfWork.productRepository.Update(p);
                unitOfWork.save();
                toaster.AddSuccessToastMessage("Proizvod je uspesno azuriran!");
                return RedirectToAction("Index", "CMS");
            }
            return RedirectToAction("Index", "CMS");
        }
    }
}
