using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Data;

namespace bookverse.Controllers
{
    public class CoverTypeController : Controller
    {
        private IToastNotification toast;
        private IUnitOfWork unitOfWork;
        public CoverTypeController(IUnitOfWork _unitOfWork, IToastNotification _toast)
        {
            toast = _toast;
            unitOfWork = _unitOfWork;
        }
        public IActionResult GetAll() {

            var cover = unitOfWork.coverTypeRepository.GetAll();
            return Json(cover);
        
        }
        public IActionResult GetOne(int id)
        {
            var cover = unitOfWork.coverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            return Json(cover);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CoverType ct) 
        {
            if(ModelState.IsValid)
            {
                unitOfWork.coverTypeRepository.Add(ct); 
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste kreirali povez!");
                return RedirectToAction("CoverTypeView", "CMS");            
            }
            toast.AddErrorToastMessage("Greska prilikom kreiranja!");
            return RedirectToAction("CoverTypeView", "CMS");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CoverType ct) {
            if (ModelState.IsValid)
            {
                unitOfWork.coverTypeRepository.Update(ct);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste azurirali povez!");
                return RedirectToAction("CoverTypeView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom azuriranja!");
            return RedirectToAction("CoverTypeView", "CMS");

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) {

            CoverType ct=unitOfWork.coverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            if (ct != null)
            {
                unitOfWork.coverTypeRepository.Delete(ct);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste izbrisali povez!");
                return RedirectToAction("CoverTypeView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom brisanja!");
            return RedirectToAction("CoverTypeView", "CMS");
        }
    }
}
