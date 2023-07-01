using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;

namespace bookverse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork unitOfWork;
		private IToastNotification toastNotification;
		public HomeController(ILogger<HomeController> logger,IUnitOfWork _unitOfWork, IToastNotification _toastNotification)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
            toastNotification = _toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }
        public IActionResult Contact() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmail(Newsletter ns)
        {
            unitOfWork.newsletterRepository.Add(ns);
            unitOfWork.save();
            toastNotification.AddSuccessToastMessage("Uspesno ste se pretplatili!");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}