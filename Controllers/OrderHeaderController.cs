using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace bookverse.Controllers
{
    public class OrderHeaderController : Controller
    {
        private IToastNotification toast;
        private IUnitOfWork unitOfWork;

        public OrderHeaderController(IUnitOfWork _unitOfWork, IToastNotification _toast)
        {
            toast = _toast;
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        
        public IActionResult GetAll()
        {
            var order=unitOfWork.orderHeaderRepository.GetAll(includeProperties: "ApplicationUser");
            return Json(order);
        }
        [HttpGet]
        
        public IActionResult GetOne(int id)
        {
            var order=unitOfWork.orderHeaderRepository.GetFirstOrDefault(x=>x.Id==id);  
            return Json(order);
        }

        [HttpGet]
        
        public IActionResult GetDetails(int id) {

            var details = unitOfWork.orderDetailsRepository.GetAll(includeProperties:"Product").Where(x => x.OrderId == id);
            return Json(details);
        
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
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
        public IActionResult Update(OrderHeader orderHeader)
        {
            if(ModelState.IsValid)
            {
                unitOfWork.orderHeaderRepository.Update(orderHeader);
                unitOfWork.save();
                toast.AddSuccessToastMessage("Uspesno ste azurirali narudzbinu!");
                return RedirectToAction("OrderHeaderView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom azuriranja!");
            return RedirectToAction("OrderHeaderView", "CMS");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id) { 
        
            OrderHeader orderHeader=unitOfWork.orderHeaderRepository.GetFirstOrDefault(x => x.Id == id);
            if (orderHeader != null)
            {
                unitOfWork.orderHeaderRepository.Delete(orderHeader);
                toast.AddSuccessToastMessage("Uspesno ste izbrisali narudzbinu!");
                return RedirectToAction("OrderHeaderView", "CMS");
            }
            toast.AddErrorToastMessage("Greska prilikom brisanja!");
            return RedirectToAction("OrderHeaderView", "CMS");

        }


    }
}
