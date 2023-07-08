using bookverse.Models;
using bookverse.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace bookverse.Controllers
{
    public class CMSController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CMSController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            IEnumerable<SelectListItem> temp = unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            IEnumerable<SelectListItem> tempC = unitOfWork.coverTypeRepository.GetAll().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

            ViewBag.CoverList = tempC;
            ViewBag.CategoryList = temp;

            ViewBag.productList = unitOfWork.productRepository.GetAll(includeProperties: "c,ct").ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryView()
        {
            
            return View("Categories");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CoverTypeView()
        {
            
            return View("CoverTypes");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult OrderHeaderView()
        {
            
            return View("Orders");
        }
    }
}
