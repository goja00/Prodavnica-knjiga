using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace bookverse.Controllers
{
    public class OrderDetailsController : Controller
    {
        private IToastNotification toast;
        private IUnitOfWork unitOfWork;

        public OrderDetailsController(IUnitOfWork _unitOfWork, IToastNotification _toast)
        {
            toast = _toast;
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        
        public IActionResult GetAll()
        {
            var data = unitOfWork.orderDetailsRepository.GetAll(includeProperties: "Product");
            return Json(data);
        }
        [HttpGet]
        
        public IActionResult GetOne(int id)
        {
            var order = unitOfWork.orderDetailsRepository.GetFirstOrDefault(x => x.Id == id);
            return Json(order);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(OrderDetails orderDetails)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.orderDetailsRepository.Add(orderDetails);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste kreirali narudzbinu!");
                return RedirectToAction("OrderHeaderView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom kreiranja!");
            return RedirectToAction("OrderHeaderView", "CMS");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.orderDetailsRepository.Update(orderDetails);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste azurirali narudzbinu!");
                return RedirectToAction("OrderHeaderView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom azuriranja!");
            return RedirectToAction("OrderHeaderView", "CMS");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {

            OrderDetails orderDetails = unitOfWork.orderDetailsRepository.GetFirstOrDefault(x => x.Id == id);
            if (orderDetails != null)
            {
                unitOfWork.orderDetailsRepository.Delete(orderDetails);
                toast.AddSuccessToastMessage("Uspesno ste izbrisali narudzbinu!");
                return RedirectToAction("OrderHeaderView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom brisanja!");
            return RedirectToAction("OrderHeaderView", "CMS");

        }
    }
}
