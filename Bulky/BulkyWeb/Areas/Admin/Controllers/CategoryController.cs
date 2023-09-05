using Bulky.DataAccess;
using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = (SD.Role_Admin))]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.categoryRepository.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display order cannot exactly match the Name.");
            }
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "test is an invalid value.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }


            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }


            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.categoryRepository.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.categoryRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
