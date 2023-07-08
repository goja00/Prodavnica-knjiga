using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace bookverse.Controllers
{
	public class ContactController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private IToastNotification toast;
		public ContactController(IUnitOfWork _unitOfWork, IToastNotification _toast)
        {
			unitOfWork = _unitOfWork;
			toast = _toast;
		}
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult saveMessage(CustomerMessage customerMessage)
		{
			if(ModelState.IsValid)
			{
				unitOfWork.customerMessageRepository.Add(customerMessage);
				unitOfWork.save();
				toast.AddSuccessToastMessage("Uspesno ste poslali poruku!");
				return RedirectToAction("Index","Home");
			}
			toast.AddErrorToastMessage("Greska prilikom slanja poruke!");
			return RedirectToAction("Index", "Home");
		}
	}
}
