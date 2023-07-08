using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace bookverse.Controllers
{
    public class CategoryController : Controller
    {
        private IToastNotification toast;
        private IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork _unitOfWork, IToastNotification _toast)
        {
            toast = _toast;
            unitOfWork= _unitOfWork;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Category category) 
        {
            if(ModelState.IsValid)
            {
                unitOfWork.categoryRepository.Add(category);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Kategorija je uspesno dodata!");
                return RedirectToAction("CategoryView", "CMS");
            }
            else
            {
                toast.AddErrorToastMessage("Doslo je do greske pri kreiranju kategorije!");
                return RedirectToAction("CategoryView", "CMS");
            }

        }

        [HttpGet]
        
        public IActionResult GetOne(int id)
        {
            var category=unitOfWork.categoryRepository.GetFirstOrDefault(x=>x.Id==id);

            return Json(category);
            
        }
        [HttpGet]
       
        public IActionResult GetAll()
        {
            var category = unitOfWork.categoryRepository.GetAll();

            return Json(category);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Category c) { 
        
            if(ModelState.IsValid)
            {
                unitOfWork.categoryRepository.Update(c);   
                unitOfWork.save();
                toast.AddSuccessToastMessage("Kategorija je uspesno azurirana!");
                return RedirectToAction("CategoryView", "CMS");
            }
            toast.AddErrorToastMessage("Doslo je do greske pri kreiranju kategorije!");
            return RedirectToAction("CategoryView", "CMS");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var cat = unitOfWork.categoryRepository.GetFirstOrDefault(x => x.Id == id);
            if (cat != null)
            {
                unitOfWork.categoryRepository.Delete(cat);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Proizvod je uspesno izbrisan!");
            return RedirectToAction("CategoryView", "CMS");
            }
            toast.AddErrorToastMessage("Doslo je do greske pri kreiranju kategorije!");
            return RedirectToAction("CategoryView", "CMS");
        }

    }
}
