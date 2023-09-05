using Bulky.DataAccess;
using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objCompanyList = _unitOfWork.companyRepository.GetAll().ToList();
            
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            //ViewBag.CategoryList = CategoryList;
            //ViewData[nameof(CategoryList)] = CategoryList;

            
            if(id == null || id == 0)
            {
                // create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.companyRepository.Get(u => u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                

                if (companyObj.Id == 0)
                {
                    _unitOfWork.companyRepository.Add(companyObj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.companyRepository.Update(companyObj);
                    TempData["success"] = "Company updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
            
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Company? obj = _unitOfWork.companyRepository.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.companyRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.companyRepository.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var CompanyToBeDeleted = _unitOfWork.companyRepository.Get(u => u.Id == id);
            if(CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.companyRepository.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Deleted Successfully." });
        }
        #endregion
    }
}
